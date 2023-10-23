using System;
using System.Collections.Generic;

namespace PawMates.DAL.Models;

public partial class Pet
{
    public int Id { get; set; }

    public int PetParentId { get; set; }

    public int PetTypeId { get; set; }

    public string Name { get; set; } = null!;

    public string? Breed { get; set; }

    public int Age { get; set; }

    public string? PostalCode { get; set; }

    public virtual PetParent PetParent { get; set; } = null!;

    public virtual PetType PetType { get; set; } = null!;

    public virtual ICollection<PlayDate> PlayDates { get; set; } = new List<PlayDate>();
}
