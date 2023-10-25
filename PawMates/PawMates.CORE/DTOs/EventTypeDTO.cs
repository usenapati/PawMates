using System.ComponentModel.DataAnnotations;

namespace PawMates.CORE.DTOs
{
    public class EventTypeDTO
    {
        [Required]
        public int Id { get; set; }
        public int? RestrictionTypeId { get; set; }
        [Required]
        public int PetTypeId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
