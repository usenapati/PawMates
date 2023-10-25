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
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required, MaxLength(255)]
        public string Description { get; set; }
    }
}
