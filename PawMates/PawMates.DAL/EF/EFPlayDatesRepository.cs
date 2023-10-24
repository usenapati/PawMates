using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PawMates.CORE;
using PawMates.CORE.Exceptions;
using PawMates.CORE.Interfaces;
using PawMates.CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PawMates.DAL.EF
{
    public class EFPlayDatesRepository : IPlayDateRepository
    {

        private readonly PawMatesContext _context;

        public EFPlayDatesRepository(PawMatesContext context)
        {
            this._context= context;
        }

        public Response<PlayDate> Add(PlayDate entity)
        {
            var response = new Response<PlayDate>() { Success = false };
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
                    response.Message = $"Pet {petId} is not exist.";
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
                    response.Message = $"Pet {petId} is not exist.";
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
                response.Data = _context.PlayDates.Include(a => a.Pets).ToList();
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
                var PlayDate = _context.PlayDates.Include(a => a.Pets).FirstOrDefault(a => a.Id == id);
                //var PlayDate = _context.PlayDates.FirstOrDefault(a => a.PlayDateId == PlayDateId);
                if (PlayDate == null)
                {
                    response.Message = "PlayDate not found.";
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
                throw new DALException("Unable to get entity", ex);
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
    }
}
