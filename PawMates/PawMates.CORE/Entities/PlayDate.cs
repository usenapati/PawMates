using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawMates.CORE.Entities
{
    public class PlayDate
    {
        public int Id { get; set; }
        public int PetParentId { get; set; }
        public int LocationId { get; set; }
        public int EventTypeId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }
}
