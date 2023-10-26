using PawMates.CORE.DTOs;
using PawMates.PlayDateAPI.ApiClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawMates.Integration.Tests
{
    internal class MockPetsService : IPetsService
    {
        private List<PetDTO> _pets;
        public MockPetsService()
        {
            this._pets = new List<PetDTO>()
            {
                new PetDTO
                {
                    Id = 1,
                    ParentId = 1,
                    PetTypeId = 1,
                    Age = 3,
                    Name = "Maruki",
                    Breed = "Tabby",
                    PostalCode = "11111",
                },
                new PetDTO
                {
                    Id = 2,
                    ParentId = 1,
                    PetTypeId = 1,
                    Age = 5,
                    Name = "Whiskers",
                    Breed = "Ragdoll",
                    PostalCode = "22222",
                }
            };
        }
        public async Task<PetDTO>? GetPetAsync(int petId)
        {
            return this._pets.FirstOrDefault(x => x.Id == petId);
        }

        public async Task<List<PetDTO>?> GetPetsAsync()
        {
            return this._pets;  
        }
    }
}
