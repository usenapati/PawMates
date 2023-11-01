using Microsoft.AspNetCore.Mvc;
using PawMates.CORE.DTOs;
using PawMates.CORE.Interfaces;
using PawMates.CORE.Mappers;
using PawMates.CORE.Models;

namespace PawMates.RestrictionTypeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestrictionTypesController : Controller
    {
        private readonly IRepository<RestrictionType> _repo;
        public RestrictionTypesController(IRepository<RestrictionType> repo)
        {
            _repo = repo;
        }

        // GET: api/<RestrictionTypesController>
        [HttpGet]
        public IActionResult Get()
        {
            var getResult = _repo.GetAll();
            if (!getResult.Success)
            {
                return NotFound();
            }
            var RestrictionTypes = _repo.GetAll().Data.ToList();

            return Ok(RestrictionTypes.Select(a => a.MapToDto()).ToList());
        }

        // GET api/<RestrictionTypesController>/5
        [HttpGet("{id}", Name = "GetRestrictionType")]
        public IActionResult GetById(int id)
        {
            var getResult = _repo.GetById(id);
            if (!getResult.Success || getResult.Data == null)
            {
                return NotFound();
            }
            RestrictionType RestrictionType = getResult.Data;
            return Ok(RestrictionType.MapToDto());

        }

        // POST api/<RestrictionTypesController>
        [HttpPost]
        // [Authorize]
        public IActionResult Post([FromBody] RestrictionTypeDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var RestrictionType = value.MapToEntity();
            _repo.Add(RestrictionType);
            return CreatedAtAction(nameof(GetById), new { id = RestrictionType.Id }, RestrictionType.MapToDto());
        }

        // PUT api/<RestrictionTypesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] RestrictionTypeDTO value)
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
            var RestrictionType = getResult.Data;
            RestrictionType.Name = value.Name;


            _repo.Update(RestrictionType);
            return NoContent();
        }

        // DELETE api/<RestrictionTypesController>/5
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
