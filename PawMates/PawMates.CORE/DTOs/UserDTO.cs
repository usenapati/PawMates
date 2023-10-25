using System.ComponentModel.DataAnnotations;

namespace PawMates.CORE.DTOs
{
    public class UserDTO
    {
        [Required]
        public int Id { get; set; }
        public int? PetParentId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
    }
}
