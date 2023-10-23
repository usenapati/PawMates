using System;
using System.Collections.Generic;

namespace PawMates.DAL.Models;

public partial class PlayDate
{
    public int Id { get; set; }

    public int PetParentId { get; set; }

    public int LocationId { get; set; }

    public int EventTypeId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public virtual EventType EventType { get; set; } = null!;

    public virtual Location Location { get; set; } = null!;

    public virtual PetParent PetParent { get; set; } = null!;

    public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();
}
