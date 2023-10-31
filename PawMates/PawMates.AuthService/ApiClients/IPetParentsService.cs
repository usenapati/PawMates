using PawMates.CORE.DTOs;

namespace PawMates.AuthService.ApiClients
{
    public interface IPetParentsService
    {
        Task<List<PetParentDTO>?> GetPetParentsAsync();

        Task<PetParentDTO>? GetPetParentAsync(int petParentId);
    }
}
