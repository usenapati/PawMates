using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PawMates.Gateway
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.Authority = builder.Configuration["Jwt:ValidIssuer"];
                    options.Audience = builder.Configuration["Jwt:ValidAudience"];

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
                        ValidAudience = builder.Configuration["Jwt:ValidAudience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:IssuerSigningKey"]))
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = ValidateToken,
                    };
                });
            builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
            builder.Services.AddOcelot(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            await app.UseOcelot();

            app.Run();
        }

        public static string GetTokenFromHeader(IHeaderDictionary requestHeaders)
        {
            if (!requestHeaders.TryGetValue("Authorization", out var authorizationHeader))
                throw new InvalidOperationException("Authorization token does not exists");

            var authorization = authorizationHeader.FirstOrDefault()!.Split(" ");

            var type = authorization[0];

            if (type != "Bearer") throw new InvalidOperationException("You should provide a Bearer token");

            var value = authorization[1] ?? throw new InvalidOperationException("Authorization token does not exists");
            return value;
        }

        public static Task ValidateToken(MessageReceivedContext context)
        {
            try
            {
                context.Token = GetTokenFromHeader(context.Request.Headers);

                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(context.Token, context.Options.TokenValidationParameters, out var validatedToken);

                var jwtSecurityToken = validatedToken as JwtSecurityToken;

                context.Principal = new ClaimsPrincipal();

                Debug.Assert(jwtSecurityToken != null, nameof(jwtSecurityToken) + " != null");

                var claimsIdentity = new ClaimsIdentity(jwtSecurityToken.Claims.ToList(), "JwtBearerToken",
                    ClaimTypes.NameIdentifier, ClaimTypes.Role);

                context.Principal.AddIdentity(claimsIdentity);

                context.Success();

                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                context.Fail(e);
            }

            return Task.CompletedTask;
        }
    }
}