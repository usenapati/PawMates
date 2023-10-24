using PawMates.CORE;
using PawMates.CORE.Models;

namespace PawMates.DAL.EF
{
    public class EFPlayDateRepository : EFRepository<PlayDate>
    {
        private readonly PawMatesContext _context;
        public EFPlayDateRepository(PawMatesContext context) : base(context)
        {
            _context = context;
        }

        public Response<IEnumerable<PlayDate>> GetByLocation(Location location)
        {
            var response = new Response<IEnumerable<PlayDate>>() { Success = false };
            try
            {
                var getResponse = _context.Locations.FirstOrDefault(l => l.Id == location.Id);
                if (getResponse == null)
                {
                    response.Message = "Location not found.";
                    return response;
                }
                var playDates = GetAll(p => p.LocationId == location.Id);
                response.Data = playDates.Data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public Response<IEnumerable<PlayDate>> GetByDate(DateTime date)
        {
            var response = new Response<IEnumerable<PlayDate>>() { Success = false };
            try
            {
                var startOfDay = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
                var endOfDay = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59); ;
                var playDates = GetAll(p => p.StartTime >= startOfDay && p.EndTime <= endOfDay);
                response.Data = playDates.Data;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public Response AddPet(int id, Pet pet)
        {
            var response = new Response() { Success = false };
            try
            {
                var getResponse = GetById(id);
                if (!getResponse.Success)
                {
                    response.Message = getResponse.Message;
                    return response;
                }
                getResponse.Data.Pets.Add(pet);
                var updateResponse = Update( getResponse.Data);
                if (!updateResponse.Success)
                {
                    response.Message = getResponse.Message;
                    return response;
                }
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public Response RemovePet(int id, Pet pet)
        {
            var response = new Response() { Success = false };
            try
            {
                var getResponse = GetById(id);
                if (!getResponse.Success)
                {
                    response.Message = getResponse.Message;
                    return response;
                }
                getResponse.Data.Pets.Remove(pet);
                var updateResponse = Update( getResponse.Data);
                if (!updateResponse.Success)
                {
                    response.Message = getResponse.Message;
                    return response;
                }
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
