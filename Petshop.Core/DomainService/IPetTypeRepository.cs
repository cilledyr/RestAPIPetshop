using Petshop.Core.Enteties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop.Core.DomainService
{
    public interface IPetTypeRepository
    {
        public List<PetType> GetAllPetTypes();
        public List<PetType> FindPetTypeById(int id);
        public List<PetType> FindPetTypeByName(string name);
        public PetType AddNewPetType(PetType theNewType);
        public PetType UpdatePetType(PetType theNewPetType, PetType theOldPetType);
        public PetType DeletePetType(PetType toBeDeletedPetType);
        public List<Pet> FindAllPetsByType(PetType theType);
    }
}
