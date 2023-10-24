using PawMates.CORE.Interfaces;

namespace PawMates.CORE.Models;

public partial class PetType : IEntity
{
    public int Id { get; set; }

    public string Species { get; set; } = null!;

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();

    public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();
}
