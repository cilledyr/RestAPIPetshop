﻿using Petshop.Core.Enteties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop.Core.ApplicationService
{
    public interface IPetService
    {
        public List<Pet> GetAllPets();
        public Pet AddNewPet(string thePetName, int theSelectedSpecies, string theColour, DateTime theSelectedBirthday, DateTime theSelectedPurchaseDate, string thePreviousOwner, long thePetPrice, Owner theOwner);
        public Pet DeletePetByID(int theId);
        public List<Pet> FindPetsByName(string theName);
        public Pet FindPetByID(int theId);
        public Pet UpdatePet(Pet updatedPet, int toUpdateInt, string updateValue);
        public List<Pet> GetSortedPets();
        public List<Pet> SearchForPet(int toSearchInt, string searchValue);
        
    }
}
