using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string Name { get; set; }
        public string? Breed { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string PostalCode { get; set; }
    }
}
