using PawMates.CORE;
using PawMates.CORE.Exceptions;
using PawMates.CORE.Interfaces;
using PawMates.CORE.Models;

namespace PawMates.DAL.EF
{
    public class EFParentRepository : IParentRepository
    {
        private readonly PawMatesContext _context;
        public EFParentRepository(PawMatesContext context)
        {
            _context = context;
        }

        public Response<IEnumerable<Pet>> GetPets(PetParent petParent)
        {
            var response = new Response<IEnumerable<Pet>>() { Success = false };
            try
            {
                var getResponse = GetById(petParent.Id);
                if (!getResponse.Success)
                {
                    response.Message = getResponse.Message;
                    return response;
                } 
                response.Data = petParent.Pets;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public Response<IEnumerable<PetParent>> GetAll()
        {
            IEnumerable<PetParent> parents;
            try
            {
                parents = _context.PetParents.ToList();
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<PetParent>>() { Message = "Unable to get Pet parents.\n" + ex.Message, Success = false };
            }

            return new Response<IEnumerable<PetParent>>() { Data = parents, Success = true };
        }

        public Response<PetParent> GetById(int id)
        {
            PetParent parent;
            try
            {
                parent = _context.PetParents.FirstOrDefault(x => x.Id == id);
                if (parent == null)
                {
                    return new Response<PetParent>() { Message = "Pet parent could not found.\n", Success = false };
                }
            }
            catch (Exception ex)
            {
                return new Response<PetParent>() { Message = "Unable to get Pet parent.\n" + ex.Message, Success = false };
            }
            return new Response<PetParent>() { Data = parent, Success = true };
        }

        public Response<PetParent> Add(PetParent parent)
        {
            try
            {
                _context.PetParents.Add(parent);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return new Response<PetParent>() { Message = "Unable to add Pet parent.\n" + ex.Message, Success = false };
            }
            return new Response<PetParent>() { Data = parent, Success = true };
        }

        public Response Delete(int id)
        {
            try
            {
                var response = GetById(id);
                if (!response.Success)
                {
                    return new Response() { Message = "Pet parent could not found.\n", Success = false };
                }
                _context.Remove(response.Data);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return new Response() { Message = "Unable to delete Pet parent.\n" + ex.Message, Success = false };
            }
            return new Response() { Success = true };

        }

        public Response Update(PetParent parent)
        {
            try
            {
                var response = GetById(parent.Id);
                if (!response.Success)
                {
                    return new Response() { Message = "Pet parent could not found.\n", Success = false };
                }
                _context.PetParents.Update(parent);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return new Response() { Message = "Unable to update Pet parent.\n" + ex.Message, Success = false };
            }
            return new Response() { Success = true };
        }

        // Add a method to get all entities that match a predicate
        public Response<IEnumerable<PetParent>> GetAll(Func<PetParent, bool> predicate)
        {
            IEnumerable<PetParent> parents;
            try
            {
                parents = _context.PetParents.Where(predicate).ToList();
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<PetParent>>() { Message = "Unable to get Pet parents.\n" + ex.Message, Success = false };
            }

            return new Response<IEnumerable<PetParent>>() { Data = parents, Success = true };
        }

        // add a method to get one entity that matches a predicate
        public Response<PetParent> GetOne(Func<PetParent, bool> predicate)
        {
            PetParent parent;
            try
            {
               parent = _context.PetParents.FirstOrDefault(predicate);
            }
            catch (Exception ex)
            {
                return new Response<PetParent>() { Message = "Unable to get Pet parent.\n" + ex.Message, Success = false };
            }
            return new Response<PetParent>() { Data = parent, Success = true };
        }

    }
}
