using PawMates.CORE.Interfaces;

namespace PawMates.CORE.Models;

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

    public override bool Equals(object? obj)
    {
        return obj is Location location &&
               Id == location.Id &&
               PetTypeId == location.PetTypeId &&
               Name == location.Name &&
               Street1 == location.Street1 &&
               City == location.City &&
               State == location.State &&
               PostalCode == location.PostalCode &&
               PetAge == location.PetAge;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, PetTypeId, Name, Street1, City, State, PostalCode, PetAge);
    }
}
