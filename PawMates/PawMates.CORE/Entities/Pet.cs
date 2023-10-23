using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawMates.CORE.Entities
{
    public class Pet
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int PetTypeId { get; set; }
        public string Name { get; set; }
        public string? Breed { get; set; }
        public int Age { get; set; }
        public string PostalCode { get; set; }
    }
}
