using PawMates.CORE.Interfaces;

namespace PawMates.CORE.Models;

public partial class PlayDate : IEntity
{
    public int Id { get; set; }

    public int PetParentId { get; set; }

    public int LocationId { get; set; }

    public int EventTypeId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public virtual EventType EventType { get; set; } = null!;

    public virtual Location Location { get; set; } = null!;

    public virtual PetParent PetParent { get; set; } = null!;

    public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();

    public override bool Equals(object? obj)
    {
        return obj is PlayDate date &&
               Id == date.Id &&
               PetParentId == date.PetParentId &&
               LocationId == date.LocationId &&
               EventTypeId == date.EventTypeId &&
               StartTime == date.StartTime &&
               EndTime == date.EndTime;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, PetParentId, LocationId, EventTypeId, StartTime, EndTime);
    }
}
