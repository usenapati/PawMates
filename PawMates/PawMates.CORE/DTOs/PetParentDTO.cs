﻿using System.ComponentModel.DataAnnotations;

namespace PawMates.CORE.DTOs
{
    public class PetParentDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string? PhoneNumber { get; set; }

        [Required]
        [MaxLength(255)]
        public string? ImageUrl { get; set; }
        [Required]
        [MaxLength(255)]
        public string? Description { get; set; }
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        [Required]
        [MaxLength(50)]
        public string State { get; set; }
        [Required]
        [MaxLength(15)]
        public string PostalCode { get; set; }

    }
}

