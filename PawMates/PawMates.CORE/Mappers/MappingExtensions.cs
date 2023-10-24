using PawMates.CORE.DTOs;
using PawMates.CORE.Models;

namespace PawMates.CORE.Mappers
{
    public static class MappingExtensions
    {
        public static PetDTO MapToDto(this Pet source)
        {
            return new PetDTO
            {
                Id = source.Id,
                ParentId = source.PetParentId,
                PetTypeId = source.PetTypeId,
                Name = source.Name,
                Breed = source.Breed,
                Age = source.Age,
                PostalCode = source.PostalCode
            };
        }
        public static Pet MapToEntity(this PetDTO source)
        {
            return new Pet
            {
                Id = source.Id,
                PetParentId = source.ParentId,
                PetTypeId = source.PetTypeId,
                Name = source.Name,
                Breed = source.Breed,
                Age = source.Age,
                PostalCode = source.PostalCode

            };
        }
    }
}
