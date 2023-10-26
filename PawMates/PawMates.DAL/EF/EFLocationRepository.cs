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

        public Response<Location> Add(Location entity)
        {
            var validateResponse = Validate(entity);
            if (!validateResponse.Success)
            {
                return validateResponse;
            }
            return base.Add(entity);
        }

        public Response Update(Location entity)
        {
            var validateResponse = Validate(entity);
            if (!validateResponse.Success)
            {
                return validateResponse;
            }
            return base.Update(entity);
        }

        private Response<Location> Validate(Location entity)
        {
            Response<Location> validateResponse = new Response<Location>() { Success = false };
            // Location is Required
            if (entity == null)
            {
                validateResponse.Message = "Location is Required.";
                return validateResponse;
            }
            // Pet Type Id must be positive
            if (entity.PetTypeId <= 0)
            {
                validateResponse.Message = "Pet Type Id must be positive.";
                return validateResponse;
            }
            // Location Name is required
            if (string.IsNullOrWhiteSpace(entity.Name))
            {
                validateResponse.Message = "Location Name is required.";
                return validateResponse;
            }
            // Location Street Name is required
            if (string.IsNullOrWhiteSpace(entity.Street1))
            {
                validateResponse.Message = "Location Street Name is required.";
                return validateResponse;
            }
            // Location City is required
            if (string.IsNullOrWhiteSpace(entity.City))
            {
                validateResponse.Message = "Location City is required.";
                return validateResponse;
            }
            // Location State is required
            if (string.IsNullOrWhiteSpace(entity.State))
            {
                validateResponse.Message = "Location State is required.";
                return validateResponse;
            }
            // Location Postal Code is required
            if (string.IsNullOrWhiteSpace(entity.PostalCode))
            {
                validateResponse.Message = "Location Postal Code is required.";
                return validateResponse;
            }
            // Location Minimum Pet Age must be postive
            if (entity.PetAge < 0)
            {
                validateResponse.Message = "Location Minimum Pet Age must be postive.";
                return validateResponse;
            }
            return new Response<Location>() { Success = true, Data = entity };
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
