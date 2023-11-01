using Microsoft.EntityFrameworkCore;
using PawMates.CORE;
using PawMates.CORE.Exceptions;
using PawMates.CORE.Interfaces;
using PawMates.CORE.Models;

namespace PawMates.DAL.EF
{
    public class EFPlayDatesRepository : IPlayDateRepository
    {

        private readonly PawMatesContext _context;

        public EFPlayDatesRepository(PawMatesContext context)
        {
            _context = context;
        }

        public Response<PlayDate> Add(PlayDate entity)
        {
            var response = new Response<PlayDate>() { Success = false };
            var validateResponse = Validate(entity);
            if (!validateResponse.Success)
            {
                response.Message = validateResponse.Message;
                return response;
            }
            try
            {
                _context.PlayDates.Add(entity);
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

        public Response AddPetToPlayDate(int playDateId, int petId)
        {
            Response response = new Response() { Success = false };
            try
            {
                var getResponse = GetById(playDateId);
                if (!getResponse.Success)
                {
                    response.Message = getResponse.Message;
                    return response;
                }

                Pet? getPet = _context.Pets.Find(petId);
                if (getPet == null)
                {
                    response.Message = $"Pet {petId} does not exist.";
                    return response;
                }
                // Handle Duplicates
                bool PetInPlayDate = getResponse.Data.Pets.Any(x => x.Id == petId);
                if (PetInPlayDate)
                {
                    response.Success = true;
                    return response;
                }
                getResponse.Data.Pets.Add(getPet);
                _context.SaveChanges();
                response.Success = true;
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
                var getResponse = GetById(playDateId);
                if (!getResponse.Success)
                {
                    response.Message = getResponse.Message;
                    return response;
                }

                Pet? getPet = _context.Pets.Find(petId);
                if (getPet == null)
                {
                    response.Message = $"Pet {petId} does not exist.";
                    return response;
                }

                bool PetInPlayDate = getResponse.Data.Pets.Any(x => x.Id == petId);
                if (!PetInPlayDate)
                {
                    response.Success = true;
                    return response;
                }
                getResponse.Data.Pets.Remove(getPet);
                _context.SaveChanges();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public Response Delete(PlayDate entity)
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

                List<int> petIds = getResponse.Data.Pets.Select(x => x.Id).ToList();
                if (petIds.Count > 0)
                {
                    foreach (var petId in petIds)
                    {
                        DeletePetFromPlayDate(entity.Id, petId);
                    }
                }

                _context.PlayDates.Remove(getResponse.Data);
                _context.SaveChanges();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }


        public Response<IEnumerable<PlayDate>> GetAll()
        {
            var response = new Response<IEnumerable<PlayDate>>() { Success = false };
            try
            {
                response.Data = _context.PlayDates.Include(a => a.Pets)
                                                  .Include(p => p.Location)
                                                  .Include(p => p.PetParent)
                                                  .Include(p => p.EventType)
                                                  .ToList();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public Response<IEnumerable<PlayDate>> GetAll(Func<PlayDate, bool> predicate)
        {
            var response = new Response<IEnumerable<PlayDate>>() { Success = false };
            try
            {
                response.Data = _context.PlayDates.Include(a => a.Pets).Where(predicate).ToList();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public Response<PlayDate> GetById(int id)
        {
            var response = new Response<PlayDate>() { Success = false };
            try
            {
                var PlayDate = _context.PlayDates.Include(a => a.Pets)
                                                 .Include(p => p.Location)
                                                 .Include(p => p.PetParent)
                                                 .Include(p => p.EventType)
                                                 .FirstOrDefault(a => a.Id == id);
                
                if (PlayDate == null)
                {
                    response.Message = "Play Date not found.";
                    return response;
                }
                response.Data = PlayDate;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public Response<PlayDate> GetOne(Func<PlayDate, bool> predicate)
        {
            var response = new Response<PlayDate>() { Success = false };
            try
            {
                response.Data = _context.PlayDates.Include(a => a.Pets).FirstOrDefault(predicate);
                response.Success = true;

            }
            catch (Exception ex)
            {
                throw new DALException($"Unable to get Play Date", ex);
            }
            return response;
        }

        public Response Update(PlayDate entity)
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
                var validateResponse = Validate(entity);
                if (!validateResponse.Success)
                {
                    response.Message = validateResponse.Message;
                    return response;
                }
                _context.PlayDates.Update(entity);
                _context.SaveChanges();
                response.Success = true;

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        private Response<PlayDate> Validate(PlayDate entity)
        {
            // Play Date is required
            if (entity == null)
            {
                return new Response<PlayDate>() { Success = false, Message = "Play Date is required." };
            }
            // Pet Parent Host Id must be positive
            if (entity.PetParentId <= 0)
            {
                return new Response<PlayDate>() { Success = false, Message = "Pet Parent Host Id must be positive." };
            }
            // Location Id must be positive
            if (entity.LocationId <= 0)
            {
                return new Response<PlayDate>() { Success = false, Message = "Location Id must be positive." };
            }
            // Event Type Id must be positive
            if (entity.EventTypeId <= 0)
            {
                return new Response<PlayDate>() { Success = false, Message = "Event Type Id must be positive." };
            }
            // Play Date End Time should be after Start Time
            if (entity.EndTime <= entity.StartTime)
            {
                return new Response<PlayDate>() { Success = false, Message = "Play Date End Time should be after Start Time." };
            }
            return new Response<PlayDate>() { Success = true, Data = entity };
        }
    }
}
