using Petshop.Core.Enteties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop.Core.DomainService
{
    public interface IPetRepository
    {
        public IEnumerable<Pet> GetAllPets();
        public Pet AddNewPet(Pet theNewPet);
        public Pet DeletePet(Pet toBeDeletedPet);
        public IEnumerable<Pet> FindPetsByName(string theName);
        public List<Pet> FindPetByID(int theId);
        public Pet UpdateNameOfPet(Pet updatedPet, string updateValue);
        public Pet UpdateColorOfPet(Pet updatedPet, string updateValue);
        public Pet UpdateTypeOfPet(Pet updatedPet, PetType updateValue);
        public Pet UpdateBirthdayOfPet(Pet updatedPet, DateTime updateValue);
        public Pet UpdateSoldDateOfPet(Pet updatedPet, DateTime updateValue);
        public Pet UpdatePreviousOwnerOfPet(Pet updatedPet, string updateValue);
 
        public Pet UpdatePriceOfPet(Pet updatedPet, long updateValue);
        public Pet UpdateFullPet(Pet theOldPet, Pet theNewPet);
        public IEnumerable<Pet> GetSortedPets();
        public IEnumerable<Pet> FindPetsByColor(string searchValue);
        public IEnumerable<Pet> SearchPetsByBirthYear(DateTime theDateValue);
        public IEnumerable<Pet> FindPetsBySoldDate(DateTime theSoldValue);
        public IEnumerable<Pet> FindPetsByPreviousOwner(string searchValue);
        public IEnumerable<Pet> FindPetsByPrice(long thePriceValue);

        public Pet UpdateOwnerOfPet(Pet updatedPet, Owner newOwner);

    }
}
