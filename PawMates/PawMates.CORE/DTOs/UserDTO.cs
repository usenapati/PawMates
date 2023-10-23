using System.ComponentModel.DataAnnotations;

namespace PawMates.CORE.DTOs
{
    public class UserDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
    }
}
