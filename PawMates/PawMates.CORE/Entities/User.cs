using System;
using System.Collections.Generic;

namespace PawMates.DAL.Models;

public partial class User
{
    public int Id { get; set; }

    public int PetParentId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual PetParent PetParent { get; set; } = null!;
}
