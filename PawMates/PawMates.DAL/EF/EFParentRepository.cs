using PawMates.CORE;
using PawMates.CORE.Models;

namespace PawMates.DAL.EF
{
    public class EFParentRepository : EFRepository<PetParent>
    {
        private readonly PawMatesContext _context;
        public EFParentRepository(PawMatesContext context) : base(context)
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
    }
}
