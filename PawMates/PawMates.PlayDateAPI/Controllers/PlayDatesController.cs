using Microsoft.AspNetCore.Mvc;
using PawMates.CORE.DTOs;
using PawMates.CORE.Exceptions;
using PawMates.CORE.Interfaces;
using PawMates.CORE.Mappers;
using PawMates.CORE.Models;
using PawMates.PlayDateAPI.ApiClients;

namespace PawMates.PlayDateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayDatesController : ControllerBase
    {
        private readonly IPlayDateRepository _repo;
        private readonly IPetsService _petsService;
        public PlayDatesController(IPlayDateRepository repo, IPetsService petsService)
        {
            this._repo = repo;
            this._petsService = petsService;
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

            return Ok(PlayDates.Select(a => a.MapToDto2()).ToList());
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

            return Ok(playDate.MapToDto2());

        }

        // /api/Playdates/{playdateId}/Pets - GET - # Returns Pets on a Playdate
        [HttpGet, Route("{playdateId}/pets")]
        public IActionResult GetPetsOnPlaydate(int playdateId)
        {
            var existResult = _repo.GetById(playdateId);
            if (!existResult.Success || existResult.Data == null)
            {
                return BadRequest($"Playdate {playdateId} is not exist.");
            }

            return Ok(existResult.Data.Pets.Select(a => a.MapToDto()).ToList());
        }

        // /api/playDates/{playDateId}/pets/{petId} -POST - # Add the pet to the playDate
        [HttpPost, Route("{playDateId}/pets/{petId}")]
        //[Authorize]
        public async Task<IActionResult> PostPetOnPlayDate(int playDateId, int petId)
        {
            var playDate = _repo.GetById(playDateId).Data;
            if (playDate == null)
            {
                return BadRequest($"playDate {playDateId} is not exist.");
            }

            PetDTO pet = await _petsService.GetPetAsync(petId);

            if (pet == null)
            {
                return BadRequest($"pet {petId} is not exist.");
            }

            var addResult = _repo.AddPetToPlayDate(playDateId, petId);

            if (!addResult.Success)
            {
                return BadRequest($"Failed to add pet.");
            }
            return NoContent();
        }

        // /api/playDates/{playDateId}/pets/{petId} - DELETE - # Remove the pet from the playDate
        [HttpDelete, Route("{playDateId}/pets/{petId}")]
        //[Authorize]
        public async Task<IActionResult> RemovePetOnPlayDate(int playDateId, int petId)
        {
            var playDate = _repo.GetById(playDateId).Data;
            if (playDate == null)
            {
                return BadRequest($"playDate {playDateId} is not exist.");
            }

            PetDTO pet = await _petsService.GetPetAsync(petId);

            if (pet == null)
            {
                return BadRequest($"pet {petId} is not exist.");
            }

            var addResult = _repo.DeletePetFromPlayDate(playDateId, petId);

            if (!addResult.Success)
            {
                return BadRequest($"Failed to add pet.");
            }
            return NoContent();
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

            // PlayDate cannot overlap with existing play date by host within their time range
            var existingPlayDate = _repo.GetAll(
                p => p.PetParentId == value.PetParentId && 
                ((p.StartTime >= value.StartTime && p.StartTime <= value.EndTime) || (p.EndTime >= value.StartTime && p.EndTime <= value.EndTime)));
            if (existingPlayDate != null && existingPlayDate.Success && existingPlayDate.Data.Count() > 0)
            {
                return BadRequest("Play Date interferes with existing Play Date by same host.");
            }
            // Event Type and Location have matching Restrictions

            var playDate = value.MapToEntity();
            try
            {
                _repo.Add(playDate);
                foreach (var hostPets in value.HostPets)
                {
                    _repo.AddPetToPlayDate(playDate.Id, hostPets);
                }
                foreach (var hostPets in value.InvitedPets)
                {
                    _repo.AddPetToPlayDate(playDate.Id, hostPets);
                }
            }
            catch (Exception ex)
            {

                throw new DALException("Unable to add PlayDate", ex);
            }
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
