using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawMates.CORE.Entities
{
    public class EventType
    {
        public int Id { get; set; }
        public int RestrictionTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
