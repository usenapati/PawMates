using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PawMates.DAL;
using PawMates.PlayDateAPI.ApiClients;

namespace PawMates.Integration.Tests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
           builder.ConfigureServices(services =>
            {
                // Find the descriptor for the original DbContext
                var dbContextDescriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<PawMatesContext>));

                // Remove the original DbContext registration
                if (dbContextDescriptor != null)
                {
                    services.Remove(dbContextDescriptor);
                }

                // Register the in-memory DbContext
                services.AddDbContext<PawMatesContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");

                });
                // Unload real service and load in mock 
                // Play Date Services
                //Add Mock PetAPI for async test
                services.AddScoped<IPetsService, MockPetsService>();
            });

            base.ConfigureWebHost(builder);
        }
    }
}
