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
                PostalCode = source.PostalCode,
                ImageUrl = source.ImageUrl,
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
                PostalCode = source.PostalCode,
                ImageUrl = source.ImageUrl,

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

        public static LocationDTO MapToDto(this Location source)
        {
            return new LocationDTO
            {
                Id = source.Id,
                PetTypeId = source.PetTypeId,
                Name = source.Name,
                Street1 = source.Street1,
                City = source.City,
                State = source.State,
                PostalCode = source.PostalCode,
                PetAge = source.PetAge
            };
        }
        public static Location MapToEntity(this LocationDTO source)
        {
            return new Location
            {
                Id = source.Id,
                PetTypeId = source.PetTypeId,
                Name = source.Name,
                Street1 = source.Street1,
                City = source.City,
                State = source.State,
                PostalCode = source.PostalCode,
                PetAge = source.PetAge
            };
        }

        public static UserDTO MapToDto(this User source)
        {
            return new UserDTO
            {
                Id = source.Id,
                PetParentId = source.PetParentId,
                Username = source.Username,
                Password = source.Password 

            };
        }
        public static User MapToEntity(this UserDTO source)
        {
            return new User
            {
                Id = source.Id,
                PetParentId = source.PetParentId,
                Username = source.Username,
                Password = source.Password
            };
        }

        public static PetParentDTO MapToDTO(this PetParent source)
        {
            return new PetParentDTO
            {
                Id = source.Id,
                FirstName = source.FirstName,
                LastName = source.LastName,
                Email = source.Email,
                PhoneNumber = source.PhoneNumber
            };
        }

        public static PetParent MapToEntity(this PetParentDTO source)
        {
            return new PetParent
            {
                Id = source.Id,
                FirstName = source.FirstName,
                LastName = source.LastName,
                Email = source.Email,
                PhoneNumber = source.PhoneNumber
            };
        }
        public static EventTypeDTO MapToDto(this EventType source)
        {
            return new EventTypeDTO
            {
                Id= source.Id,
                RestrictionTypeId = source.RestrictionTypeId,
                PetTypeId = source.PetTypeId,
                Name= source.Name,
                Description= source.Description,
            };
        }

        public static EventType MapToEntity(this EventTypeDTO source)
        {
            return new EventType
            {
                Id = source.Id,
                RestrictionTypeId = source.RestrictionTypeId,
                PetTypeId = source.PetTypeId,
                Name= source.Name,
                Description= source.Description,
            };
        }
    }
}
