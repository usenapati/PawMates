using PawMates.CORE.Interfaces;

namespace PawMates.CORE.Models;

public partial class PetParent : IEntity
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string Email { get; set; } = null!;

    public string? ImageUrl { get; set; }
    public string? Description { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }

    public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();

    public virtual ICollection<PlayDate> PlayDates { get; set; } = new List<PlayDate>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public override bool Equals(object? obj)
    {
        return obj is PetParent parent &&
               Id == parent.Id &&
               FirstName == parent.FirstName &&
               LastName == parent.LastName &&
               PhoneNumber == parent.PhoneNumber &&
               Email == parent.Email;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, FirstName, LastName, PhoneNumber, Email);
    }
}
