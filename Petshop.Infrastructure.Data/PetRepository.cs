using Petshop.Core.DomainService;
using Petshop.Core.Enteties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Petshop.Infrastructure.Data
{
    public class PetRepository : IPetRepository
    {
        
        public IEnumerable<Pet> GetAllPets()
        {
            return PetDB.allThePets;
        }

        public Pet AddNewPet(Pet theNewPet)
        {
            return PetDB.AddNewPet(theNewPet);
        }

        public Pet DeletePetById(int theId)
        {
            return PetDB.DeletePetById(theId);
        }

        public IEnumerable<Pet> FindPetsByName(string theName)
        {
            IEnumerable<Pet> petsByName = PetDB.allThePets.Where(pet => pet.PetName.Contains(theName));
            return petsByName;
        }

        public List<Pet> FindPetByID(int theId)
        {
            return (PetDB.allThePets.Where(pet => pet.PetId == theId)).ToList();
            
        }

        public Pet UpdateNameOfPet(Pet updatedPet, string updateValue)
        {
            return PetDB.UpdateNameOfPet(updatedPet, updateValue);
        }

        public Pet UpdateColorOfPet(Pet updatedPet, string updateValue)
        {
            return PetDB.UpdateColourOfPet(updatedPet, updateValue);
        }

        public Pet UpdateSpeciesOfPet(Pet updatedPet, Pet.Species updateValue)
        {
            return PetDB.UpdateSpeciesOfPet(updatedPet, updateValue);
        }

        public Pet UpdateBirthdayOfPet(Pet updatedPet, DateTime updateValue)
        {
            return PetDB.UpdateBirthdayOfPet(updatedPet, updateValue);
        }

        public Pet UpdateSoldDateOfPet(Pet updatedPet, DateTime updateValue)
        {
            return PetDB.UpdateSoldDateOfPet(updatedPet, updateValue);
        }

        public Pet UpdatePreviousOwnerOfPet(Pet updatedPet, string updateValue)
        {
            return PetDB.UpdatePreviousOwnerOfPet(updatedPet, updateValue);
        }

        public Pet UpdatePriceOfPet(Pet updatedPet, long updateValue)
        {
            return PetDB.UpdatePriceOfPet(updatedPet, updateValue);
        }

        public Pet UpdateFullPet(Pet theOldPet, Pet theNewPet)
        {
            return PetDB.UpdateFullPet(theOldPet, theNewPet);
        }

        public IEnumerable<Pet> GetSortedPets()
        {
            IEnumerable<Pet> sortedPets = PetDB.allThePets.OrderBy(pet => pet.PetPrice);
            return sortedPets;
        }

        public IEnumerable<Pet> FindPetsByColor(string searchValue)
        {
            IEnumerable<Pet> petsByColor = PetDB.allThePets.Where(pet => pet.PetColor.Equals(searchValue));
            return petsByColor;
        }

        public IEnumerable<Pet> FindPetsBySpecies(Pet.Species theSearchCriteria)
        {
            IEnumerable<Pet> petsBySpecies = PetDB.allThePets.Where(pet => pet.PetSpecies == theSearchCriteria);
            return petsBySpecies;
        }

        public IEnumerable<Pet> SearchPetsByBirthYear(DateTime theDateValue)
        {
            IEnumerable<Pet> petsByBirthyear = PetDB.allThePets.Where(pet => pet.PetBirthday.Year == theDateValue.Year);
            return petsByBirthyear;
        }

        public IEnumerable<Pet> FindPetsBySoldDate(DateTime theSoldValue)
        {
            IEnumerable<Pet> petsBySoldyear = PetDB.allThePets.Where(pet => pet.PetSoldDate.Year == theSoldValue.Year);
            return petsBySoldyear;
        }

        public IEnumerable<Pet> FindPetsByPreviousOwner(string searchValue)
        {
            IEnumerable<Pet> petsByPreviousOwners = PetDB.allThePets.Where(pet => pet.PetPreviousOwner.Contains(searchValue));
            return petsByPreviousOwners;
        }

        public IEnumerable<Pet> FindPetsByPrice(long thePriceValue)
        {
            IEnumerable<Pet> petsByPrice = PetDB.allThePets.Where(pet => pet.PetPrice <= thePriceValue - 10 && pet.PetPrice <= thePriceValue + 10 );
            return petsByPrice;
        }

        public Pet UpdateOwnerOfPet(Pet updatedPet, int ownerId)
        {
            return PetDB.UpdateOwnerOfPet(updatedPet, ownerId);
        }

        
    }
}
