﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawMates.CORE.DTOs
{
    public class PlayDateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int PetParentId { get; set; }
        [Required]
        public int LocationId { get; set; }
        [Required]
        public int EventTypeId { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
    }
}