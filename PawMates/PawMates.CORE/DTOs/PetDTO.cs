using System.ComponentModel.DataAnnotations;

namespace PawMates.CORE.DTOs
{
    public class PetDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int ParentId { get; set; }
        [Required]
        public int PetTypeId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string? Breed { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        [MaxLength(10)]
        public string? PostalCode { get; set; }
    }
}
