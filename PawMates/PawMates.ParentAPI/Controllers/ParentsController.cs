using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PawMates.CORE.DTOs;
using PawMates.CORE.Interfaces;
using PawMates.CORE.Mappers;
using PawMates.CORE.Models;
using PawMates.DAL.EF;
using PawMates.PlayDateAPI.ApiClients;

namespace PawMates.ParentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentsController : ControllerBase
    {
        private readonly IParentRepository _parentRepository;
        private readonly IPetsService _petsService;

        public ParentsController(IParentRepository parentRepository, IPetsService petsService)
        {
            _parentRepository = parentRepository;
            _petsService = petsService;
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
                PetParent existingPetParent = _parentRepository.GetAll().Data.FirstOrDefault(x => x.Email == parent.Email);
                if (existingPetParent != null)
                {
                    return BadRequest("Pet Parent with given email already exists");
                }
                var petParent = _parentRepository.Add(parent.MapToEntity());
                
                return CreatedAtAction(nameof(Get), new { id = petParent.Data.Id }, petParent.Data.MapToDTO());
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
                parent.ImageUrl = value.ImageUrl;
                parent.Description = value.Description;
                parent.City = value.City;
                parent.State = value.State;
                parent.PostalCode = value.PostalCode;
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

        // GET /api/<ParentsController>/{parentId}/Pets
        [HttpGet, Route("{parentId}/pets")]
        public async Task<IActionResult> GetPets(int parentId)
        {
            try
            {
                var parent = _parentRepository.GetById(parentId);
                if (!parent.Success)
                {
                    return BadRequest(parent.Message);
                }
                if (parent.Data == null)
                {
                    return NotFound();
                }
                var petList = await _petsService.GetPetsAsync();
                var pets = petList.Where(p => p.ParentId == parentId);
                if (pets.IsNullOrEmpty())
                {
                    return NotFound();
                }
                return Ok(pets);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST /api/<ParentsController>/{parentId}/Pets/{petId}
        [HttpPost, Route("{parentId}/pets/{petId}")]
        public async Task<IActionResult> PostPet(int parentId, int petId)
        {
            try
            {
                var parent = _parentRepository.GetById(parentId);
                if (parent.Data == null)
                {
                    return NotFound();
                }

                PetDTO pet = await _petsService.GetPetAsync(petId);

                if (pet == null)
                {
                    return NotFound();
                }

                var add = _parentRepository.AddPetToParent(parentId, petId);

                if (!add.Success)
                {
                    return BadRequest(add.Message);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE /api/<ParentsController>/{parentId}/Pets/{petId}
        [HttpDelete, Route("{parentId}/pets/{petId}")]
        public async Task<IActionResult> DeletePet(int parentId, int petId)
        {
            try
            {
                var parent = _parentRepository.GetById(parentId);
                if (parent.Data == null)
                {
                    return NotFound();
                }

                PetDTO pet = await _petsService.GetPetAsync(petId);

                if (pet == null)
                {
                    return NotFound();
                }

                var delete = _parentRepository.DeletePetFromParent(parentId, petId);

                if (!delete.Success)
                {
                    return BadRequest(delete.Message);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
