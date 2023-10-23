using System.ComponentModel.DataAnnotations;

namespace PawMates.CORE.Entities
{
    public class User
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
