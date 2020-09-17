using Petshop.Core.Enteties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop.Core.ApplicationService
{
    public interface IPetService
    {
        public List<Pet> GetAllPets();
        public Pet AddNewPet(Pet newPet);
        public Pet DeletePetByID(int theId);
        public List<Pet> FindPetsByName(string theName);
        public Pet FindPetByID(int theId);
        public Pet UpdatePet(int updatePetId, UpdateModel update);
        public Pet UpdatePet(Pet thePet);
        public List<Pet> GetSortedPets();
        public List<Pet> SearchForPet(FilterModel filter);
        
    }
}
