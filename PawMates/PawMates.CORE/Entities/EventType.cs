using PawMates.CORE.Interfaces;

namespace PawMates.CORE.Models;

public partial class EventType : IEntity
{
    public int Id { get; set; }

    public int? RestrictionTypeId { get; set; }

    public int PetTypeId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<PlayDate> PlayDates { get; set; } = new List<PlayDate>();

    public virtual RestrictionType? RestrictionType { get; set; }

    public virtual PetType PetType { get; set; }
}

