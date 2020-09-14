using Petshop.Core.DomainService;
using Petshop.Core.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Petshop.Core.ApplicationService.Impl
{
    public class PetTypeService : IPetTypeService
    {
        private IPetTypeRepository _petTypeRepo;

        public PetTypeService(IPetTypeRepository petTypeRepository)
        {
            _petTypeRepo = petTypeRepository;
        }
        public PetType AddNewPetType(PetType theNewType)
        {
            return _petTypeRepo.AddNewPetType(theNewType);
        }

        public PetType DeletePetType(int id)
        {
            PetType toBeDeleted = FindPetTypeById(id);
                return _petTypeRepo.DeletePetType(toBeDeleted);
            
        }

        public List<Pet> FindAllPetsByType(PetType theType)
        {
            return _petTypeRepo.FindAllPetsByType(theType);
        }

        public PetType FindPetTypeById(int id)
        {
            List<PetType> allthePetsWithID = _petTypeRepo.FindPetTypeById(id);
            if(allthePetsWithID.Count != 1)
            {
                throw new Exception("COuld not find pet with that Id");
            }
            else
            {
                return allthePetsWithID[0];
            }
        }

        public PetType FindPetTypeByIdWithPets(int id)
        {
            List<PetType> foundPetType = _petTypeRepo.FindPetTypeById(id);
            if (foundPetType.Count != 1)
            {
                return null;
            }
            else
            {
                PetType thePetType = foundPetType.Select(pt => new PetType()
                {
                    PetTypeId = pt.PetTypeId,
                    PetTypeName = pt.PetTypeName,
                    PetTypePets = FindAllPetsByType(pt)

                }).FirstOrDefault(pt => pt.PetTypeId == id);

                return thePetType;
            }
        }

        public List<PetType> FindPetTypeByName(string name)
        {
            return _petTypeRepo.FindPetTypeByName(name);
        }

        public List<PetType> GetALlPetTypes()
        {
            return _petTypeRepo.GetAllPetTypes();
        }

        public PetType UpdatePetType(PetType theUpdatedType)
        {
            PetType theOldPetType = FindPetTypeById(theUpdatedType.PetTypeId);
            return _petTypeRepo.UpdatePetType(theUpdatedType, theOldPetType);
        }
    }
}
