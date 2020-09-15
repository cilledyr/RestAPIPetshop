using Petshop.Core.Enteties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop.Core.ApplicationService
{
    public interface IPetService
    {
        public List<Pet> GetAllPets();
        public Pet AddNewPet(string thePetName, PetType theNewType, string theColour, DateTime theSelectedBirthday, DateTime theSelectedPurchaseDate, string thePreviousOwner, double thePetPrice, int theOwnerId);
        public Pet DeletePetByID(int theId);
        public List<Pet> FindPetsByName(string theName);
        public Pet FindPetByID(int theId);
        public Pet UpdatePet(int updatePetId, int toUpdateInt, string updateValue);
        public Pet UpdatePet(Pet thePet);
        public List<Pet> GetSortedPets();
        public List<Pet> SearchForPet(int toSearchInt, string searchValue);
        
    }
}
