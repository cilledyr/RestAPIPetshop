using Petshop.Core.DomainService;
using Petshop.Core.Enteties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Petshop.Core.ApplicationService.Impl
{
    public class PetService : IPetService
    {

        private IPetRepository _petRepo;
        private IOwnerRepository _ownerRepo;
        private IPetTypeRepository _petTypeRepo;
        public PetService(IPetRepository petRepository, IOwnerRepository ownerRepository, IPetTypeRepository petTypeRepository)
        {
            _petRepo = petRepository;
            _ownerRepo = ownerRepository;
            _petTypeRepo = petTypeRepository;
        }


        // Adds a new pet, Owner and Type are added unless their ID is given, in which case they are found by Id.
        public Pet AddNewPet(Pet theNewPet)
        {
            
            List<PetType> theType = null;
            if(theNewPet.PetType.PetTypeId != 0)
            {
                theType = _petTypeRepo.FindPetTypeById(theNewPet.PetType.PetTypeId);
                if(theType.Count != 1)
                {
                    throw new Exception("Sorry can't find that type.");
                }
            }
            else
            {
                theType[0] = _petTypeRepo.AddNewPetType(theNewPet.PetType);
            }
            
            if(theType.Count != 1)
            {
                throw new Exception(message: "Could not find the type.");
            }
            else
            {
                theNewPet.PetType = theType[0];
            }

            List<Owner> theOwners = null;
            if (theNewPet.PetOwner.OwnerId == 0)
            {
                theOwners[0] = _ownerRepo.AddNewOwner(theNewPet.PetOwner);
            }
            else
            {
                theOwners = _ownerRepo.FindOwnerByID(theNewPet.PetOwner.OwnerId);
            }

            if(theOwners.Count != 1)
            {
                throw new Exception(message: "Could not find the right owner");
            }
            else
            {
                theNewPet.PetOwner = theOwners[0];
            }

            return  _petRepo.AddNewPet(theNewPet);

            
        }



        public Pet DeletePetByID(int theId)
        {
            List<Pet> toBeDeletedPets = _petRepo.FindPetByID(theId);
            if(toBeDeletedPets.Count != 1 || toBeDeletedPets[0] == null)
            {
                throw new InvalidDataException(message: "Could not find that pet Id to delete.");
            }
            return _petRepo.DeletePet(toBeDeletedPets[0]);
        }



        public Pet FindPetByID(int theNewId)
        {
            List<Pet> thePets = _petRepo.FindPetByID(theNewId);
            if (thePets.Count != 1)
            {
                return null;
            }
            else
            {
                return thePets[0];

            }
        }

        public List<Pet> FindPetsByName(string theName)
        {
            return _petRepo.FindPetsByName(theName).ToList();
        }


        public List<Pet> GetAllPets()
        {
            return _petRepo.GetAllPets().ToList();
            
        }

        public List<Pet> GetSortedPets()
        {
            return _petRepo.GetSortedPets().ToList();
        }


        //Searches for pets, for owner and type both id, and name are valid searches.
        public List<Pet> SearchForPet(FilterModel filter)
        {
            string searchValue = filter.SearchValue;
            string toSearchString = filter.SearchTerm.ToLower();
            switch (toSearchString)
            {
                case "name":
                    return _petRepo.FindPetsByName(searchValue).ToList();
                case "color":
                    return _petRepo.FindPetsByColor(searchValue).ToList();
                case "type":
                    int theSearch;
                    List<PetType> thePetType = null;
                    if (int.TryParse(searchValue, out theSearch) && theSearch != 0)
                    {
                        thePetType = _petTypeRepo.FindPetTypeById(theSearch);
                        if(thePetType.Count < 1 )
                        {
                            throw new Exception(message: "Sorry could not find id of that PetType.");
                        }
                        else
                        {
                            return _petTypeRepo.FindAllPetsByType(thePetType[0]);
                        }

                    }
                    else
                    {
                        thePetType = _petTypeRepo.FindPetTypeByName(searchValue);
                        if (thePetType.Count <  1)
                        {
                            throw new Exception(message: "Sorry could not find name of that PetType.");
                        }
                        else
                        {
                            List<Pet> allPetsByType = null;
                            foreach (var petType in thePetType)
                            {
                                if(allPetsByType == null)
                                {
                                    allPetsByType = _petTypeRepo.FindAllPetsByType(petType);
                                }
                                else
                                {
                                    allPetsByType = allPetsByType.Concat(_petTypeRepo.FindAllPetsByType(petType)).ToList();
                                }
                            }
                            return allPetsByType;
                        }
                    }

                case "birthday":
                    DateTime theDateValue = DateTime.Now;
                    if (DateTime.TryParse(searchValue, out theDateValue))
                    {
                        return _petRepo.SearchPetsByBirthYear(theDateValue).ToList() ;
                    }
                    else
                    {
                        throw new InvalidDataException(message: "You have not entered a valid date.");
                    }

                case "solddate":
                    DateTime theSoldValue = DateTime.Now;
                    if (DateTime.TryParse(searchValue, out theSoldValue))
                    {
                        return _petRepo.FindPetsBySoldDate(theSoldValue).ToList() ;
                    }
                    else
                    {
                        throw new InvalidDataException(message: "You have not entered a valid date.");
                    }
                case "previousowner":
                    return _petRepo.FindPetsByPreviousOwner(searchValue).ToList();

                case "owner":
                    int theSearchForOwner;
                    List<Owner> thePetOwner = null;
                    if (int.TryParse(searchValue, out theSearchForOwner) && theSearchForOwner != 0)
                    {
                        thePetOwner = _ownerRepo.FindOwnerByID(theSearchForOwner);
                        if (thePetOwner.Count < 1)
                        {
                            throw new Exception(message: "Sorry could not find id of that PetType.");
                        }
                        else
                        {
                            return _ownerRepo.FindAllPetsByOwner(thePetOwner[0]);
                        }

                    }
                    else
                    {
                        thePetOwner = _ownerRepo.FindOwnerByName(searchValue).ToList();
                        if (thePetOwner.Count < 1)
                        {
                            throw new Exception(message: "Sorry could not find name of that PetType.");
                        }
                        else
                        {
                            List<Pet> allPetsByTheOwners = null;
                            foreach (var owner in thePetOwner)
                            {
                                if(allPetsByTheOwners == null)
                                {
                                    allPetsByTheOwners = _ownerRepo.FindAllPetsByOwner(owner);
                                }
                                else
                                {
                                    allPetsByTheOwners.Concat(_ownerRepo.FindAllPetsByOwner(owner));
                                }
                            }
                            return allPetsByTheOwners;
                        }
                    }
                case "price":
                    long thePriceValue = 0;
                    if (long.TryParse(searchValue, out thePriceValue))
                    {
                        return _petRepo.FindPetsByPrice(thePriceValue).ToList();
                    }
                    else
                    {
                        throw new InvalidDataException(message: "You have not entered a valid price.");
                    }
                case "id":
                    int searchId;
                    if(int.TryParse(searchValue, out searchId) && searchId!=0)
                    {
                        return _petRepo.FindPetByID(searchId);
                    }
                    else
                    {
                        throw new InvalidDataException(message: "Id out of bounds.");
                    }
                    

                default:
                    throw new InvalidDataException(message: "I don't have that property to seach for.");
            }
        }


        //Updates pet, for type and owner, if Id is set to 0, a new one wil be created from the entered values. From values entered, instead of a whole pet.
        public Pet UpdatePet(int updatePetId, UpdateModel update)
        {
            int updateParam = update.UpdateParam.Value;
            string updateValue = update.UpdateValue;
            Pet updatedPet = FindPetByID(updatePetId);
            if(updatedPet == null)
            {
                throw new Exception(message: "I am sorry could not find that pet to Update.");
            }
            else
            {
                switch (updateParam)
                {
                    case 1:
                        return _petRepo.UpdateNameOfPet(updatedPet, updateValue);
                    case 2:
                        return _petRepo.UpdateColorOfPet(updatedPet, updateValue);
                    case 3:
                        int petTypeId;
                        List<PetType> updatedType = null;
                        if (int.TryParse(updateValue, out petTypeId))
                        {
                            updatedType = _petTypeRepo.FindPetTypeById(petTypeId);
                        }
                        else
                        {
                            updatedType = _petTypeRepo.FindPetTypeByName(updateValue);
                        }
                        if (updatedType.Count != 1)
                        {
                            throw new Exception("Can't find a PetType of that variety");
                        }
                        else
                        {
                            return _petRepo.UpdateTypeOfPet(updatedPet, updatedType[0]);
                        }

                    case 4:
                        DateTime theUpdateValue = DateTime.Now;
                        if (DateTime.TryParse(updateValue, out theUpdateValue))
                        {
                            return _petRepo.UpdateBirthdayOfPet(updatedPet, theUpdateValue);
                        }
                        else
                        {
                            throw new InvalidDataException(message: "You have not entered a valid date.");
                        }

                    case 5:
                        DateTime theSoldUpdateValue = DateTime.Now;
                        if (DateTime.TryParse(updateValue, out theSoldUpdateValue))
                        {
                            return _petRepo.UpdateSoldDateOfPet(updatedPet, theSoldUpdateValue);
                        }
                        else
                        {
                            throw new InvalidDataException(message: "You have not entered a valid date.");
                        }
                    case 6:
                        return _petRepo.UpdatePreviousOwnerOfPet(updatedPet, updateValue);
                    case 7:
                        long thePriceValue = 0;
                        if (long.TryParse(updateValue, out thePriceValue))
                        {
                            return _petRepo.UpdatePriceOfPet(updatedPet, thePriceValue);
                        }
                        else
                        {
                            throw new InvalidDataException(message: "You have not entered a valid price.");
                        }
                    case 8:
                        int id;
                        if (int.TryParse(updateValue, out id))
                        {
                            List<Owner> allTheOwners = _ownerRepo.GetAllOwners().ToList();
                            if (id != 0)
                            {
                                List<Owner> newOwners = _ownerRepo.FindOwner(id);
                                if (newOwners.Count != 1 || newOwners[0] == null)
                                {
                                    throw new Exception(message: "Could not find an owner with that Id.");
                                }
                                else
                                {
                                    return _petRepo.UpdateOwnerOfPet(updatedPet, newOwners[0]);
                                }

                            }
                            else
                            {
                                throw new InvalidDataException(message: "Id of owner out of Bounds.");
                            }
                        }
                        else
                        {
                            throw new InvalidDataException(message: "You have not entered a valid id.");
                        }
                    default:
                        throw new InvalidDataException(message: "I am sorry i do not have that parameter to update.");
                }
            }

            
        }
        //Updates pet, for type and owner, if Id is set to 0, a new one wil be created from the entered values. Takes a whole pet, returns the upated pet.
        public Pet UpdatePet(Pet thePet)
        {
            List<Pet> thePets = _petRepo.FindPetByID(thePet.PetId);
            if (thePets.Count !=1 || thePets[0] == null)
            {
                throw new Exception(message: "I am sorry could not find pet to update.");
            }
            else
            {
                if(thePet.PetOwner.OwnerId == 0)
                {
                    thePet.PetOwner = _ownerRepo.AddNewOwner(thePet.PetOwner);
                }
                else
                {
                    List<Owner> theOwners = _ownerRepo.FindOwner(thePet.PetOwner.OwnerId);
                    if(theOwners.Count != 1 || theOwners[0] == null)
                    {
                        throw new Exception(message: "Sorry wrong number of owners found with that ID.");
                    }
                    else
                    {
                        thePet.PetOwner = theOwners[0];
                    }
                }
                List<PetType> theType = null;
                if (thePet.PetType.PetTypeId == 0)
                {
                    theType = _petTypeRepo.FindPetTypeByName(thePet.PetType.PetTypeName);

                }
                else
                {
                    theType = _petTypeRepo.FindPetTypeById(thePet.PetType.PetTypeId);
                }
                if(theType.Count != 1)
                {
                    throw new Exception("Could not find any PetType with these parameters.");
                }
                
                return _petRepo.UpdateFullPet(thePets[0], thePet);
            }
        }
    }
}
