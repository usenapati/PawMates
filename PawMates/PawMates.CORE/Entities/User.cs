
using PawMates.CORE.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawMates.DAL.Models
{
    public class User : IEntity
{
        public int Id { get; set; }
        public int PetParentId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual PetParent PetParent { get; set; } = null!;
    }
}
