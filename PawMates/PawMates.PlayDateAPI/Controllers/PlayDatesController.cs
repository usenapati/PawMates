using Microsoft.AspNetCore.Mvc;
using PawMates.CORE.DTOs;
using PawMates.CORE.Interfaces;
using PawMates.CORE.Mappers;
using PawMates.CORE.Models;

namespace PawMates.PlayDateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayDatesController : ControllerBase
    {
        private readonly IRepository<PlayDate> _repo;
        public PlayDatesController(IRepository<PlayDate> repo)
        {
            this._repo = repo;
        }

        // GET: api/<PlayDatesController>
        [HttpGet]
        public IActionResult Get()
        {
            var getResult = _repo.GetAll();
            if (!getResult.Success)
            {
                return NotFound();
            }
            var PlayDates = _repo.GetAll().Data.ToList();

            return Ok(PlayDates.Select(a => a.MapToDto()).ToList());
        }

        // GET api/<PlayDatesController>/5
        [HttpGet("{id}", Name = "GetPlayDate")]
        public IActionResult GetById(int id)
        {
            var getResult = _repo.GetById(id);
            if (!getResult.Success || getResult.Data == null)
            {
                return NotFound();
            }
            PlayDate playDate = getResult.Data;
            return Ok(playDate.MapToDto());

        }

        // POST api/<PlayDatesController>
        [HttpPost]
        // [Authorize]
        public IActionResult Post([FromBody] PlayDateDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var playDate = value.MapToEntity();
            _repo.Add(playDate);
            return CreatedAtAction(nameof(GetById), new { id = playDate.Id }, playDate.MapToDto());
        }

        // PUT api/<PlayDatesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PlayDateDTO value)
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
            var PlayDate = getResult.Data;
            PlayDate.PetParentId = value.PetParentId;
            PlayDate.LocationId= value.LocationId;
            PlayDate.EventTypeId = value.EventTypeId;
            PlayDate.StartTime = value.StartTime;
            PlayDate.EndTime = value.EndTime;           

            _repo.Update(PlayDate);
            return NoContent();
        }

        // DELETE api/<PlayDatesController>/5
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
