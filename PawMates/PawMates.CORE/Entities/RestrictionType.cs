using PawMates.CORE.Interfaces;

namespace PawMates.CORE.Models;

public partial class RestrictionType : IEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<EventType> EventTypes { get; set; } = new List<EventType>();

    public override bool Equals(object? obj)
    {
        return obj is RestrictionType type &&
               Id == type.Id &&
               Name == type.Name;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name);
    }
}
