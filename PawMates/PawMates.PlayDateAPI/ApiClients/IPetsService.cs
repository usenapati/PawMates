using PawMates.CORE.DTOs;

namespace PawMates.PlayDateAPI.ApiClients
{
    public interface IPetsService
    {
        Task<List<PetDTO>?> GetPetsAsync();

        Task<PetDTO>? GetPetAsync(int petId);
    }
}
