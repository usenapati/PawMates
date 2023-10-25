using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawMates.CORE.Interfaces;
using PawMates.CORE.Models;
using PawMates.CORE.Mappers;
using PawMates.DAL.EF;
using PawMates.CORE.DTOs;

namespace PawMates.ParentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentsController : ControllerBase
    {
        private readonly EFParentRepository _parentRepository;

        public ParentsController(EFParentRepository parentRepository)
        {
            _parentRepository = parentRepository;
        }

        // GET: api/<PetParentsController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<PetParent> parents = _parentRepository.GetAll().Data;
                return Ok(parents.Select(p => p.MapToDTO()).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<PetParentsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var parent = _parentRepository.GetById(id);
                if (!parent.Success)
                {
                    return BadRequest(parent.Message);
                }
                if (parent.Data == null)
                {
                    return NotFound();
                }
                return Ok(parent.Data.MapToDTO());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<PetParentsController>
        [HttpPost]
        //[Authorize]
        public IActionResult Post([FromBody] PetParentDTO parent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _parentRepository.Add(parent.MapToEntity());
                return CreatedAtAction(nameof(Get), new { id = parent.Id }, parent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<PetParentsController>/5
        [HttpPut("{id}")]
        //[Authorize]
        public IActionResult Put(int id, [FromBody] PetParentDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var parent = _parentRepository.GetById(id).Data;
                if(parent == null)
                {
                    return NotFound();
                }
                parent.FirstName = value.FirstName;
                parent.LastName = value.LastName;
                parent.Email = value.Email;
                parent.PhoneNumber = value.PhoneNumber;
                _parentRepository.Update(parent);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<PetParentsController>/5
        [HttpDelete("{id}")]
        //[Authorize]
        public IActionResult Delete(int id)
        {
            try
            {
                var parent = _parentRepository.GetById(id).Data;
                if (parent == null)
                {
                    return NotFound();
                }
                _parentRepository.Delete(parent);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
