using PawMates.CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawMates.CORE.Interfaces
{
    public interface IPlayDateRepository : IRepository<PlayDate>
    {
        Response AddPetToPlayDate(int playDateId, int petId);           
        Response DeletePetFromPlayDate(int playDateId, int petId);
    }
}
