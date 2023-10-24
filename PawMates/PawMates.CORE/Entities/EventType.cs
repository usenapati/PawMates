using System;
using System.Collections.Generic;

namespace PawMates.DAL.Models;

public partial class EventType
{
    public int Id { get; set; }

    public int? RestrictionTypeId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<PlayDate> PlayDates { get; set; } = new List<PlayDate>();

    public virtual RestrictionType? RestrictionType { get; set; }
}

