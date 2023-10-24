using PawMates.CORE.Interfaces;

namespace PawMates.CORE.Models;

public class User : IEntity
{
    public int Id { get; set; }
    public int PetParentId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public virtual PetParent PetParent { get; set; } = null!;
}
