using PawMates.CORE.Interfaces;

namespace PawMates.CORE.Models;

public partial class RestrictionType : IEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<EventType> EventTypes { get; set; } = new List<EventType>();
}
