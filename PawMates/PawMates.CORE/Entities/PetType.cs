using System;
using System.Collections.Generic;

namespace PawMates.DAL.Models;

public partial class PetType
{
    public int Id { get; set; }

    public string Species { get; set; } = null!;

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();

    public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();
}
