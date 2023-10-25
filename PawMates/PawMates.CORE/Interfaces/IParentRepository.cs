using PawMates.CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawMates.CORE.Interfaces
{
    public interface IParentRepository
    {
        Response<IEnumerable<Pet>> GetPets(PetParent petParent);
    }
}
