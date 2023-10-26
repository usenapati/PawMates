using Microsoft.AspNetCore.Mvc;
using PawMates.CORE.DTOs;
using PawMates.CORE.Interfaces;
using PawMates.CORE.Models;
using PawMates.CORE.Mappers;

namespace PawMates.LocationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly IRepository<Location> _repo;
        public LocationsController(IRepository<Location> repo)
        {
            _repo = repo;
        }

        // GET: api/<LocationsController>
        [HttpGet]
        public IActionResult Get()
        {
            var getResult = _repo.GetAll();
            if (!getResult.Success)
            {
                return NotFound();
            }
            var Locations = _repo.GetAll().Data.ToList();

            return Ok(Locations.Select(a => a.MapToDto()).ToList());
        }

        // GET api/<LocationsController>/5
        [HttpGet("{id}", Name = "GetLocation")]
        public IActionResult GetById(int id)
        {
            var getResult = _repo.GetById(id);
            if (!getResult.Success || getResult.Data == null)
            {
                return NotFound();
            }
            Location Location = getResult.Data;
            return Ok(Location.MapToDto());

        }

        // POST api/<LocationsController>
        [HttpPost]
        // [Authorize]
        public IActionResult Post([FromBody] LocationDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Location = value.MapToEntity();
            _repo.Add(Location);
            return CreatedAtAction(nameof(GetById), new { id = Location.Id }, Location.MapToDto());
        }

        // PUT api/<LocationsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] LocationDTO value)
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
            var Location = getResult.Data;
            Location.PetTypeId = value.PetTypeId;
            Location.Name = value.Name;
            Location.Street1 = value.Street1;
            Location.City = value.City;
            Location.State = value.State;
            Location.PostalCode = value.PostalCode;
            Location.PetAge = value.PetAge;

            _repo.Update(Location);
            return NoContent();
        }

        // DELETE api/<LocationsController>/5
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
