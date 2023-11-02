using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PawMates.CORE.DTOs;
using PawMates.CORE.Interfaces;
using PawMates.CORE.Mappers;
using PawMates.CORE.Models;


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

        // /api/pets/{id}/PetParent - GET - # Returns Pet's Parent
        [HttpGet, Route("{id}/petparent")]
        public IActionResult GetPetsOnPlaydate(int id)
        {
            var existResult = _repo.GetById(id);
            if (!existResult.Success || existResult.Data == null)
            {
                return BadRequest($"Pet {id} is not exist.");
            }

            return Ok(existResult.Data.PetParent.MapToDTO());
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
            var petParentId = User.Claims.Where(x => x.Type == "PetParentId").Select(x => x.Value).FirstOrDefault();
            if (petParentId == null)
            {
                return Forbid();
            }

            var getResult = _repo.GetById(id);
            if (!getResult.Success || getResult.Data == null)
            {
                return NotFound();
            }
            var pet = getResult.Data;

            if(pet.PetParentId != int.Parse(petParentId))
            {
                return Forbid();
            }

            //pet.PetParentId = value.ParentId;
            pet.Name = value.Name;
            pet.Age = value.Age;
            pet.Breed = value.Breed;
            pet.PetTypeId = value.PetTypeId;
            pet.PostalCode = value.PostalCode;
            pet.ImageUrl = value.ImageUrl;
            pet.Description = value.Description;

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
