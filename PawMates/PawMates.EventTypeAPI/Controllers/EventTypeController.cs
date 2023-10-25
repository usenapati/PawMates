using Microsoft.AspNetCore.Mvc;
using PawMates.CORE.DTOs;
using PawMates.CORE.Exceptions;
using PawMates.CORE.Interfaces;
using PawMates.CORE.Mappers;
using PawMates.CORE.Models;

namespace PawMates.EventTypeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventTypeController : ControllerBase
    {
        private readonly IRepository<EventType> _repo;
        public EventTypeController(IRepository<EventType> repo)
        {
            _repo = repo;
        }

        // GET: api/<EventTypeController>
        [HttpGet]
        public IActionResult Get()
        {
            var getResult = _repo.GetAll();
            if (!getResult.Success)
            {
                return NotFound();
            }
            var events = getResult.Data.ToList();
            return Ok(events.Select(e => e.MapToDto()).ToList());
        }

        // GET api/<EventTypeController>/5
        [HttpGet("{id}", Name = "GetEventType")]
        public IActionResult GetById(int id)
        {
            var getResult = _repo.GetById(id);
            if (!getResult.Success || getResult.Data == null)
            {
                return NotFound();
            }
            EventType eventType = getResult.Data;
            return Ok(eventType.MapToDto());
        }

        // POST api/<EventTypeController>
        [HttpPost]
        public IActionResult Post([FromBody] EventTypeDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var eventType = value.MapToEntity();
            try
            {
                _repo.Add(eventType);
                return CreatedAtAction(nameof(GetById), new { id = eventType.Id }, eventType.MapToDto());
            }
            catch (Exception ex)
            {
                throw new DALException("Could not add Event Type", ex);
            }
        }

        // PUT api/<EventTypeController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] EventTypeDTO value)
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
            var eventType = getResult.Data;
            eventType.RestrictionTypeId = value.RestrictionTypeId;
            eventType.PetTypeId = value.PetTypeId;
            eventType.Name = value.Name;
            eventType.Description = value.Description;

            try
            {
                _repo.Update(eventType);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new DALException("Could not update Event Type", ex);
            }
        }

        // DELETE api/<EventTypeController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var getResult = _repo.GetById(id);
            if (!getResult.Success || getResult.Data == null)
            {
                return NotFound();
            }
            try
            {
                _repo.Delete(getResult.Data);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new DALException("Could not delete Event Type", ex);
            }
        }
    }
}
