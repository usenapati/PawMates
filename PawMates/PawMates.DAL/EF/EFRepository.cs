using PawMates.CORE;
using PawMates.CORE.Exceptions;
using PawMates.CORE.Interfaces;

namespace PawMates.DAL.EF
{
    public class EFRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly PawMatesContext _context;
        public EFRepository(PawMatesContext context)
        {
            _context = context;
        }

        public Response<IEnumerable<T>> GetAll()
        {
            var response = new Response<IEnumerable<T>>() { Success = false };
            try
            {
                response.Data = _context.Set<T>().ToList();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public Response<T> GetById(int id)
        {
            var response = new Response<T>() { Success = false };
            try
            {
                var entity = _context.Set<T>().FirstOrDefault(x => x.Id == id);
                if (entity == null)
                {
                    response.Message = $"{typeof(T).Name} not found.";
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

        public Response<T> Add(T entity)
        {
            var response = new Response<T>() { Success = false };
            try
            {
                _context.Add(entity);
                _context.SaveChanges();
                response.Success = true;
                response.Data = entity;
            }
            catch (Exception ex)
            {
                throw new DALException($"Unable to save {typeof(T).Name}", ex);
            }
            return response;
        }

        public Response Delete(T entity)
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
                _context.Remove(getResponse.Data);
                _context.SaveChanges();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;

        }

        public Response Update(T entity)
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
                _context.Update(entity);
                _context.SaveChanges();
                response.Success = true;

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }



        // Add a method to get all entities that match a predicate
        public Response<IEnumerable<T>> GetAll(Func<T, bool> predicate)
        {
            var response = new Response<IEnumerable<T>>() { Success = false };
            try
            {
                response.Data = _context.Set<T>().Where(predicate).ToList();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }  

        // add a method to get one entity that matches a predicate
        public Response<T> GetOne(Func<T, bool> predicate)
        {
            var response = new Response<T>() { Success = false };
            try
            {
                response.Data = _context.Set<T>().FirstOrDefault(predicate);
                response.Success = true;
               
            }
            catch (Exception ex)
            {
                throw new DALException($"Unable to get {typeof(T).Name}", ex);
            }
            return response;          
        }

  




    }
}
