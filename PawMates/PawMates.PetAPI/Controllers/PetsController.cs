using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PawMates.CORE.DTOs;
using PawMates.CORE.Interfaces;
using PawMates.CORE.Mappers;
using PawMates.DAL.Models;

namespace PawMates.PetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly IRepository<Pet> _repo;
        public PetsController(IRepository<Pet> repo)
        {
            this._repo = repo;
        }

        // GET: api/<AgentsController>
        [HttpGet]
        public IActionResult Get()
        {
            var getResult = _repo.GetAll();
            if (!getResult.Success)
            {
                return NotFound();
            }
            var agents = _repo.GetAll().Data.ToList();

            return Ok(agents.Select(a => a.MapToDto()).ToList());
        }

        // GET api/<PetsController>/5
        [HttpGet("{id}", Name = "GetPet")]
        public IActionResult GetById(int id)
        {
            var Pet = _repo.GetById(id);
            if (Pet == null)
            {
                return NotFound();
            }
            return Ok(Pet.MapToDto());

        }

        // POST api/<PetsController>
        [HttpPost]
       // [Authorize]
        public IActionResult Post([FromBody] PetDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Pet = new Pet();
       

            _repo.Add(Pet);
            //return CreatedAtRoute(nameof(GetById), new { id = Pet.Id }, Pet.MapToDto());
            return CreatedAtAction(nameof(GetById), new { id = Pet.Id }, Pet.MapToDto());
            //return Ok(Pet.MapToDto());
        }

        // PUT api/<PetsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PetDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Pet = value.MapToEntity();

            _repo.Update(Pet);
            return NoContent();
        }

        // DELETE api/<PetsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var Pet = _repo.GetById(id);
            if (Pet == null)
            {
                return NotFound();
            }
            _repo.Delete(Pet);
            return NoContent();
        }
    }
}
