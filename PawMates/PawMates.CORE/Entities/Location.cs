using PawMates.CORE.Interfaces;
using System;
using System.Collections.Generic;

namespace PawMates.DAL.Models;

public partial class Location : IEntity
{
    public int Id { get; set; }

    public int PetTypeId { get; set; }

    public string Name { get; set; } = null!;

    public string Street1 { get; set; } = null!;

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    public int PetAge { get; set; }

    public virtual PetType PetType { get; set; } = null!;

    public virtual ICollection<PlayDate> PlayDates { get; set; } = new List<PlayDate>();
}
