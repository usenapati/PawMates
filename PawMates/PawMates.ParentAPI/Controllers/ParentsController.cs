using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawMates.CORE.Interfaces;
using PawMates.CORE.Models;
using PawMates.DAL.EF;

namespace PawMates.ParentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentsController : ControllerBase
    {
        private readonly EFRepository<PetParent> _parentRepository;

        public ParentsController(EFRepository<PetParent> parentRepository)
        {
            _parentRepository = parentRepository;
        }

        // GET: api/<PetParentsController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                
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
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<PetParentsController>
        [HttpPost]
        //[Authorize]
        public IActionResult Post([FromBody] PetParentDTO agent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                
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
               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
