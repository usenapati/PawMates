using PawMates.CORE.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawMates.CORE.Interfaces
{
    public interface IReportsRepository
    {
        Response<List<TopPetParentsListItem>> GetTopPetParents();
        Response<List<PlayDatesBySpeciesListItem>> GetPlayDatesBySpecies(string species);
        Response<List<PlayDatesByLocationListItem>> GetPlayDatesByLocation(string postalCode);

    }
}
