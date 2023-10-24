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

        public static PlayDateDTO MapToDto(this PlayDate source)
        {
            return new PlayDateDTO
            {
                Id = source.Id,
                PetParentId = source.PetParentId,
                LocationId = source.LocationId,
                EventTypeId = source.EventTypeId,
                StartTime = source.StartTime,
                EndTime = source.EndTime
                
            };
        }
        public static PlayDate MapToEntity(this PlayDateDTO source)
        {
            return new PlayDate
            {
                Id = source.Id,
                PetParentId = source.PetParentId,
                LocationId = source.LocationId,
                EventTypeId = source.EventTypeId,
                StartTime = source.StartTime,
                EndTime = source.EndTime

            };
        }
    }
}
