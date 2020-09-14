using Petshop.Core.Enteties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop.Core.ApplicationService
{
    public interface IPetTypeService
    {
        public List<PetType> GetALlPetTypes();
        public PetType FindPetTypeByIdWithPets(int id);
        public PetType FindPetTypeById(int id);
        public List<PetType> FindPetTypeByName(string name);
        public PetType AddNewPetType(PetType theNewType);
        public PetType UpdatePetType(PetType theUpdatedType);
        public PetType DeletePetType(int Id);
        public List<Pet> FindAllPetsByType(PetType theType);
    }
}
