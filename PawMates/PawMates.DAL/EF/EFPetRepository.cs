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
            this._context = context;
        }

        public Response<Pet> Add(Pet entity)
        {
            var response = new Response<Pet>() { Success = false };
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
                var entity = _context.Pets.Include(a => a.PetParent).Include(a => a.PetType).FirstOrDefault(x => x.Id == id);
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
                throw new DALException("Unable to get entity", ex);
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
    }
}
