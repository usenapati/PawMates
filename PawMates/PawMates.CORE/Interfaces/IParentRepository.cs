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
        Response<Pet> AddPetToParent(int parentId, int petId);
        Response<Pet> DeletePetFromParent(int parentId, int petId);
        Response<IEnumerable<PetParent>> GetAll();
        Response<PetParent> GetById(int id);
        Response<PetParent> Add(PetParent parent);
        Response Update(PetParent parent);
        Response Delete(PetParent parent);
        Response<IEnumerable<PetParent>> GetAll(Func<PetParent, bool> predicate);
        Response<PetParent> GetOne(Func<PetParent, bool> predicate);
    }
}
