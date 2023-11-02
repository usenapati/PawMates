using PawMates.CORE.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawMates.CORE.DTOs
{
    //PlayDateDTO only for output for front-end
    public class PlayDateDTO2
    {
        public int Id { get; set; }
        public int HostId { get; set; }
        public string HostName { get; set; }
        public string LocationName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public int PetTypeId { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int NumberOfPets { get; set; }
        public virtual ICollection<PetDTO> Pets { get; set; } = new List<PetDTO>();
    }
}
