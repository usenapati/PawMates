using System;
using System.Collections.Generic;


namespace PawMates.DAL.Models;

public partial class PetParent
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string Email { get; set; } = null!;

    public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();

    public virtual ICollection<PlayDate> PlayDates { get; set; } = new List<PlayDate>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();

}
