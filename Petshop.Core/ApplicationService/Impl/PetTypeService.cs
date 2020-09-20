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
                return null;
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
            List<PetType> thePetTypes = _petTypeRepo.FindPetTypeByName(name);
            if(thePetTypes.Count <1)
            {
                throw new Exception(message: "Could not find any pettypes of that name.");
            }
            else
            {
                return thePetTypes;
            }
        }

        public List<PetType> GetALlPetTypes()
        {
            return _petTypeRepo.GetAllPetTypes().ToList();

        }

        public List<PetType> SearchPetType(FilterModel filter)
        {
            string searchTerm = filter.SearchTerm.ToLower();
            string searchValue = filter.SearchValue;
            switch(searchTerm)
            {
                case "id":
                    List<PetType> thePetTypes;
                    int theSearchId;
                    if(int.TryParse(searchValue, out theSearchId) || theSearchId == 0)
                    {
                        thePetTypes = new List<PetType> { FindPetTypeById(theSearchId) };
                        return thePetTypes;
                    }
                    else
                    {
                        throw new Exception(message: "You have not entered a valid id.");
                    }
                case "name":
                    return FindPetTypeByName(searchValue);
                default:
                    throw new Exception(message: "Could not find that search term");
            }
        }

        public PetType UpdatePetType(PetType theUpdatedType)
        {
            PetType theOldPetType = FindPetTypeById(theUpdatedType.PetTypeId);
            return _petTypeRepo.UpdatePetType(theUpdatedType, theOldPetType);
        }
    }
}
