using Petshop.Core.DomainService;
using Petshop.Core.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Petshop.Infrastructure.Data
{
    public class PetTypeReposiory : IPetTypeRepository

    {
        public PetType AddNewPetType(PetType theNewType)
        {
            return PetDB.AddNewPetType(theNewType);
        }

        public PetType DeletePetType(PetType toBeDeletedPetType)
        {
            return PetDB.DeletePetType(toBeDeletedPetType);
        }

        public List<Pet> FindAllPetsByType(PetType theType)
        {
            return PetDB.allThePets.Where(pet => pet.PetType == theType).ToList();
        }

        public List<PetType> FindPetTypeById(int id)
        {
            return PetDB.allThePetTypes.Where(petType => petType.PetTypeId == id).ToList();

        }

        public List<PetType> FindPetTypeByName(string name)
        {
            return PetDB.allThePetTypes.Where(petType => petType.PetTypeName.Contains(name)).ToList();
        }


        public List<PetType> GetAllPetTypes()
        {
            return PetDB.allThePetTypes.ToList();
        }

        public PetType UpdatePetType(PetType theNewPetType, PetType theOldPetType)
        {
            return PetDB.UpdatePetType(theNewPetType, theOldPetType);
        }
    }
}
