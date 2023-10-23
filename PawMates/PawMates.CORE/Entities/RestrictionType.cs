using System;
using System.Collections.Generic;

namespace PawMates.DAL.Models;

public partial class RestrictionType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<EventType> EventTypes { get; set; } = new List<EventType>();
}
