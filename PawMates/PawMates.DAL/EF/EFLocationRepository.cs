using PawMates.CORE;
using PawMates.CORE.Models;

namespace PawMates.DAL.EF
{
    public class EFLocationRepository : EFRepository<Location>
    {
        private readonly PawMatesContext _context;
        public EFLocationRepository(PawMatesContext context) : base(context)
        {
            _context = context;
        }

        public Response<IEnumerable<Location>> GetByPetType(PetType petType)
        {
            var response = new Response<IEnumerable<Location>>() { Success = false };
            try
            {
                var locations = _context.Locations.Where(l => l.PetType == petType);
                response.Data = locations;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public Response<IEnumerable<Location>> GetByPetAge(int minimumPetAge)
        {
            var response = new Response<IEnumerable<Location>>() { Success = false };
            try
            {
                var locations = _context.Locations.Where(l => l.PetAge >= minimumPetAge);
                response.Data = locations;
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
