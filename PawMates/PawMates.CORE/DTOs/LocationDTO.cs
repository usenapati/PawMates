using System.ComponentModel.DataAnnotations;

namespace PawMates.CORE.DTOs
{
    public class LocationDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int PetTypeId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        public string Street1 { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        public string City { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        public string State { get; set; } = null!;
        [Required]
        [MaxLength(15)]
        public string PostalCode { get; set; } = null!;
        [Range(0, 20, ErrorMessage = "Minimum Pet Age must between 0 and 20.")]
        public int PetAge { get; set; }
    }
}
