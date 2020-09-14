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



        public Pet AddNewPet(string thePetName, int theSelectedSpecies, string theColour, DateTime theSelectedBirthday, DateTime theSelectedPurchaseDate, string thePreviousOwner, double thePetPrice, int theOwnerId)
        {
            Pet theNewPet = new Pet();
            theNewPet.PetName = thePetName;
            switch (theSelectedSpecies)
            {
                case 1:
                    theNewPet.PetSpecies = Pet.Species.Dog;
                    break;
                case 2:
                    theNewPet.PetSpecies = Pet.Species.Cat;
                    break;
                case 3:
                    theNewPet.PetSpecies = Pet.Species.Fish;
                    break;
                case 4:
                    theNewPet.PetSpecies = Pet.Species.Horse;
                    break;
                case 5:
                    theNewPet.PetSpecies = Pet.Species.Hamster;
                    break;
                case 6:
                    theNewPet.PetSpecies = Pet.Species.Gerbil;
                    break;
                case 7:
                    theNewPet.PetSpecies = Pet.Species.Rabbit;
                    break;
           
                default:
                    throw new InvalidDataException(message: "You entered a Species out of bounds.");
            }
            theNewPet.PetColor = theColour;
            theNewPet.PetBirthday = theSelectedBirthday;
            theNewPet.PetSoldDate = theSelectedPurchaseDate;
            theNewPet.PetPreviousOwner = thePreviousOwner;
            theNewPet.PetPrice = thePetPrice;
            List<Owner> theOwners = _ownerRepo.FindOwner(theOwnerId);
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
                throw new Exception(message: "I am sorry wrong amonut of pets found by ID.");
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



        public List<Pet> SearchForPet(int toSearchInt, string searchValue)
        {
            switch (toSearchInt)
            {
                case 1:
                    return _petRepo.FindPetsByName(searchValue).ToList();
                case 2:
                    return _petRepo.FindPetsByColor(searchValue).ToList();
                case 3:
                    int theSearch;
                    Pet.Species theSearchCriteria = Pet.Species.Dog;
                    if (int.TryParse(searchValue, out theSearch) && theSearch >= 1 && theSearch <= 7)
                    {
                        switch (theSearch)
                        {
                            case 1:
                                theSearchCriteria = Pet.Species.Dog;
                                break;
                            case 2:
                                theSearchCriteria = Pet.Species.Cat;
                                break;
                            case 3:
                                theSearchCriteria = Pet.Species.Fish;
                                break;
                            case 4:
                                theSearchCriteria = Pet.Species.Horse;
                                break;
                            case 5:
                                theSearchCriteria = Pet.Species.Hamster;
                                break;
                            case 6:
                                theSearchCriteria = Pet.Species.Gerbil;
                                break;
                            case 7:
                                theSearchCriteria = Pet.Species.Rabbit;
                                break;
                            default:
                                throw new InvalidDataException(message: "Index for species is out of bounds");
                        }
                    }
                    return _petRepo.FindPetsBySpecies(theSearchCriteria).ToList();

                case 4:
                    DateTime theDateValue = DateTime.Now;
                    if (DateTime.TryParse(searchValue, out theDateValue))
                    {
                        return _petRepo.SearchPetsByBirthYear(theDateValue).ToList() ;
                    }
                    else
                    {
                        throw new InvalidDataException(message: "You have not entered a valid date.");
                    }

                case 5:
                    DateTime theSoldValue = DateTime.Now;
                    if (DateTime.TryParse(searchValue, out theSoldValue))
                    {
                        return _petRepo.FindPetsBySoldDate(theSoldValue).ToList() ;
                    }
                    else
                    {
                        throw new InvalidDataException(message: "You have not entered a valid date.");
                    }
                case 6:
                    return _petRepo.FindPetsByPreviousOwner(searchValue).ToList();

                case 7:
                    long thePriceValue = 0;
                    if (long.TryParse(searchValue, out thePriceValue))
                    {
                        return _petRepo.FindPetsByPrice(thePriceValue).ToList();
                    }
                    else
                    {
                        throw new InvalidDataException(message: "You have not entered a valid price.");
                    }
                case 9:
                    int searchId;
                    if(int.TryParse(searchValue, out searchId))
                    {
                        return _petRepo.FindPetByID(searchId);
                    }
                    else
                    {
                        throw new InvalidDataException(message: "You have not given me a Nr to search the Id's for.");
                    }
                    

                default:
                    throw new InvalidDataException(message: "Something unexpected went wrong.");
            }
        }



        public Pet UpdatePet(int updatePetId, int toUpdateInt, string updateValue)
        {
            Pet updatedPet = FindPetByID(updatePetId);

            switch (toUpdateInt)
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
                    if(updatedType.Count != 1)
                    {
                        throw new Exception("Can't find a PetType of that variety");
                    }
                    else
                    {
                        return _petRepo.UpdateTypeOfPet(updatedPet, updatedType[0]);
                    }

                case 4:
                    DateTime theUpdateValue = DateTime.Now;
                    if(DateTime.TryParse(updateValue, out theUpdateValue))
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
                    if(int.TryParse(updateValue, out id))
                    {
                        List<Owner> allTheOwners = _ownerRepo.GetAllOwners().ToList();
                        if(id != 0)
                        {
                            List<Owner> newOwners = _ownerRepo.FindOwner(id);
                            if(newOwners.Count != 1 || newOwners[0] == null)
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
                    throw new InvalidDataException(message: "Something unexpected went wrong.");
            }
        }

        public Pet UpdatePet(Pet thePet)
        {
            List<Pet> thePets = _petRepo.FindPetByID(thePet.PetId);
            if (thePets.Count !=1 || thePets[0] == null)
            {
                throw new Exception(message: "I am sorry wrong amount of pets found by ID.");
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
