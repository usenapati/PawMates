using Microsoft.AspNetCore.Mvc;
using PawMates.CORE.DTOs;
using PawMates.CORE.Interfaces;
using PawMates.CORE.Mappers;
using PawMates.CORE.Models;

namespace PawMates.PetTypeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetTypesController : Controller
    {
        private readonly IRepository<PetType> _repo;
        public PetTypesController(IRepository<PetType> repo)
        {
            _repo = repo;
        }

        // GET: api/<PetTypesController>
        [HttpGet]
        public IActionResult Get()
        {
            var getResult = _repo.GetAll();
            if (!getResult.Success)
            {
                return NotFound();
            }
            var PetTypes = _repo.GetAll().Data.ToList();

            return Ok(PetTypes.Select(a => a.MapToDto()).ToList());
        }

        // GET api/<PetTypesController>/5
        [HttpGet("{id}", Name = "GetPetType")]
        public IActionResult GetById(int id)
        {
            var getResult = _repo.GetById(id);
            if (!getResult.Success || getResult.Data == null)
            {
                return NotFound();
            }
            PetType PetType = getResult.Data;
            return Ok(PetType.MapToDto());

        }

        // POST api/<PetTypesController>
        [HttpPost]
        // [Authorize]
        public IActionResult Post([FromBody] PetTypeDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var PetType = value.MapToEntity();
            _repo.Add(PetType);
            return CreatedAtAction(nameof(GetById), new { id = PetType.Id }, PetType.MapToDto());
        }

        // PUT api/<PetTypesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PetTypeDTO value)
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
            var PetType = getResult.Data;
            PetType.Species = value.Species;
            

            _repo.Update(PetType);
            return NoContent();
        }

        // DELETE api/<PetTypesController>/5
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
