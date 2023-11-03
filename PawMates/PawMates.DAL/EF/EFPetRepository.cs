using Microsoft.EntityFrameworkCore;
using PawMates.CORE;
using PawMates.CORE.Exceptions;
using PawMates.CORE.Interfaces;
using PawMates.CORE.Models;


namespace PawMates.DAL.EF
{
    public class EFPetRepository : IRepository<Pet>
    {
        private readonly PawMatesContext _context;
        public EFPetRepository(PawMatesContext context)
        {
            _context = context;
        }

        public Response<Pet> Add(Pet entity)
        {
            var response = new Response<Pet>() { Success = false };
            var validResponse = Validate(entity);
            if (!validResponse.Success)
            {
                response.Message = validResponse.Message;
                return response;
            }
            try
            {
                _context.Pets.Add(entity);
                _context.SaveChanges();
                response.Success = true;
                response.Data = entity;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public Response DeletePetFromPlayDate(int playDateId, int petId)
        {
            Response response = new Response() { Success = false };
            try
            {
                var getResponse = GetById(petId);
                if (!getResponse.Success)
                {
                    response.Message = getResponse.Message;
                    return response;
                }

                PlayDate? getPlayDate = _context.PlayDates.Find(playDateId);
                if (getPlayDate == null)
                {
                    response.Message = $"PlayDate {playDateId} does not exist.";
                    return response;
                }

                bool PetInPlayDate = getPlayDate.Pets.Any(x => x.Id == petId);
                if (!PetInPlayDate)
                {
                    response.Success = true;
                    return response;
                }
                getPlayDate.Pets.Remove(getResponse.Data);
                _context.SaveChanges();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public Response Delete(Pet entity)
        {
            Response response = new Response() { Success = false };
            try
            {
                var getResponse = GetById(entity.Id);
                if (!getResponse.Success)
                {
                    response.Message = getResponse.Message;
                    return response;
                }

                List<int> playDateIds = getResponse.Data.PlayDates.Select(x => x.Id).ToList();

                if (playDateIds.Count > 0)
                {
                    foreach (var playDateId in playDateIds)
                    {
                        DeletePetFromPlayDate(playDateId, entity.Id);
                    }
                }

                _context.Pets.Remove(getResponse.Data);
                _context.SaveChanges();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public Response<IEnumerable<Pet>> GetAll()
        {
            var response = new Response<IEnumerable<Pet>>() { Success = false };
            try
            {
                response.Data = _context.Pets.Include(p => p.PetParent).Include(a => a.PetType).ToList();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public Response<IEnumerable<Pet>> GetAll(Func<Pet, bool> predicate)
        {
            var response = new Response<IEnumerable<Pet>>() { Success = false };
            try
            {
                response.Data = _context.Pets.Include(p => p.PetParent).Include(a => a.PetType).Where(predicate).ToList();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public Response<Pet> GetById(int id)
        {
            var response = new Response<Pet>() { Success = false };
            try
            {
                var entity = _context.Pets.Include(a => a.PetParent).Include(a => a.PetType).Include(a => a.PlayDates).FirstOrDefault(x => x.Id == id);
                if (entity == null)
                {
                    response.Message = "Pet not found.";
                    return response;
                }
                response.Data = entity;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public Response<Pet> GetOne(Func<Pet, bool> predicate)
        {
            var response = new Response<Pet>() { Success = false };
            try
            {
                response.Data = _context.Pets.Include(p => p.PetParent).Include(a => a.PetType).FirstOrDefault(predicate);
                response.Success = true;

            }
            catch (Exception ex)
            {
                throw new DALException("Unable to get Pet", ex);
            }
            return response;
        }

        public Response Update(Pet entity)
        {
            Response response = new Response() { Success = false };
            try
            {
                var getResponse = GetById(entity.Id);
                if (!getResponse.Success)
                {
                    response.Message = getResponse.Message;
                    return response;
                }
                var validResponse = Validate(entity);
                if (!validResponse.Success)
                {
                    response.Message = validResponse.Message;
                    return response;
                }
                _context.Pets.Update(entity);
                _context.SaveChanges();
                response.Success = true;

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        private Response<Pet> Validate(Pet entity)
        {
            // Pet is required
            if (entity == null)
            {
                return new Response<Pet> { Success = false, Message = "Pet is required." };
            }
            // Pet Parent ID is required
            if (entity.PetParentId <= 0)
            {
                return new Response<Pet> { Success = false, Message = "Pet Parent ID is required." };
            }
            // Pet Type ID is required
            if (entity.PetTypeId <= 0)
            {
                return new Response<Pet> { Success = false, Message = "Pet Type ID is required." };
            }
            // Pet Name is required
            if (string.IsNullOrEmpty(entity.Name))
            {
                return new Response<Pet> { Success = false, Message = "Pet Name is required." };
            }
            // Pet Name is too long
            if (entity.Name.Length > 50)
            {
                return new Response<Pet> { Success = false, Message = "Pet Name is too long." };
            }
            // Pet Breed Name is too long if not null
            if (entity.Breed != null && entity.Breed.Length > 50)
            {
                return new Response<Pet> { Success = false, Message = "Pet Breed Name is too long." };
            }
            // Pet Age must be positive
            if (entity.Age < 0)
            {
                return new Response<Pet> { Success = false, Message = "Pet Age must be positive." };
            }
            // Postal Code is too long if not null
            if (entity.PostalCode != null && entity.PostalCode.Length > 10)
            {
                return new Response<Pet> { Success = false, Message = "Postal Code is too long." };
            }
            return new Response<Pet> { Success = true, Data = entity };
        }
    }
}
