using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PawMates.AuthService.ApiClients;
using PawMates.CORE.DTOs;
using PawMates.CORE.Exceptions;
using PawMates.CORE.Interfaces;
using PawMates.CORE.Mappers;
using PawMates.CORE.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PawMates.AuthService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IRepository<User> _repo;
        private readonly IPetParentsService _petParentsService;
        public AuthController(IRepository<User> repo, IPetParentsService petParentsService)
        {
            this._repo = repo;
            this._petParentsService = petParentsService;
        }

        // GET api/<ContactsController>/5
        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult Get(int id)
        {
            var getResult = _repo.GetById(id);
            if (!getResult.Success || getResult.Data == null)
            {
                return NotFound();
            }
            User user = getResult.Data;
            return Ok(user.MapToDto());
        }
        [HttpPost, Route("register")]
        public IActionResult CreateUser([FromBody] UserDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User userLogin = _repo.GetAll().Data.FirstOrDefault(x => x.Username == value.Username);
            if (userLogin != null)
            {
                return Unauthorized("Username already exists");
            }
            var user = value.MapToEntity();
            try
            {
                _repo.Add(user);
            }
            catch (Exception ex)
            {
                throw new DALException("Could not register new User", ex);
            }
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        [HttpPost, Route("login")]
        public IActionResult Login(UserDTO value)
        {
            if (value == null)
            {
                return BadRequest("Invalid request");
            }
            //
            var getUsersResult = _repo.GetAll();
            if (!getUsersResult.Success)
            {
                return NotFound();
            }
            User userLogin = getUsersResult.Data.FirstOrDefault(x => x.Username == value.Username);
            if (userLogin == null)
            {
                return Unauthorized("No matching user");
            }
            if (userLogin.Password != value.Password)
            {
                return Unauthorized("Username and password do not match");
            }

            string petParentId = "";
            if (userLogin.PetParentId != null)
            {
                petParentId = ((int)userLogin.PetParentId).ToString();
            }
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeyForSignInSecret@1234"));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "https://localhost:7061",
                audience: "https://localhost:7061",
                claims: new List<Claim>() { new Claim("PetParentId", petParentId) },
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signinCredentials);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { Token = tokenString });
        }

    }
}
