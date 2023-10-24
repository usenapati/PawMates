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

        // GET: api/<petsController>
        [HttpGet]
        public IActionResult Get()
        {
            var getResult = _repo.GetAll();
            if (!getResult.Success)
            {
                return NotFound();
            }
            var pets = _repo.GetAll().Data.ToList();

            return Ok(pets.Select(a => a.MapToDto()).ToList());
        }

        // GET api/<PetsController>/5
        [HttpGet("{id}", Name = "GetPet")]
        public IActionResult GetById(int id)
        {
            var getResult = _repo.GetById(id);
            if (!getResult.Success || getResult.Data == null)
            {
                return NotFound();
            }
            Pet pet = getResult.Data; 
            return Ok(pet.MapToDto());

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
            var pet = value.MapToEntity();
            _repo.Add(pet);            
            return CreatedAtAction(nameof(GetById), new { id = pet.Id }, pet.MapToDto());
        }

        // PUT api/<PetsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PetDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var getResult = _repo.GetById(id);
            if (!getResult.Success || getResult.Data == null)
            {
                return NotFound();
            }
            var pet = getResult.Data;
            pet.PetParentId = value.ParentId;
            pet.Name = value.Name;
            pet.Age = value.Age;
            pet.Breed = value.Breed;
            pet.PetTypeId = value.PetTypeId;
            pet.PostalCode = value.PostalCode;

            _repo.Update(pet);
            return NoContent();
        }

        // DELETE api/<PetsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var getResult = _repo.GetById(id);
            if (!getResult.Success || getResult.Data == null)
            {
                return NotFound();
            }
            _repo.Delete(getResult.Data);
            return NoContent();
        }
    }
}
