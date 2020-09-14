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

        public PetService(IPetRepository petRepository, IOwnerRepository ownerRepository)
        {
            _petRepo = petRepository;
            _ownerRepo = ownerRepository;
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
            theNewPet.PetOwner = _ownerRepo.FindOwner(theOwnerId);

            return  _petRepo.AddNewPet(theNewPet);

            
        }



        public Pet DeletePetByID(int theId)
        {
            return _petRepo.DeletePetById(theId);
        }



        public Pet FindPetByID(int theNewId)
        {
            List<Pet> thePets = _petRepo.FindPetByID(theNewId);
            if (thePets.Count <= 0 || thePets.Count > 1)
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



        public Pet UpdatePet(Pet updatedPet, int toUpdateInt, string updateValue)
        {
            switch (toUpdateInt)
            {
                case 1:
                    return _petRepo.UpdateNameOfPet(updatedPet, updateValue);
                case 2:
                    return _petRepo.UpdateColorOfPet(updatedPet, updateValue);
                case 3:
                    int theUpdate;
                    Pet.Species theupdatedValue = Pet.Species.Dog;
                    if(int.TryParse(updateValue, out theUpdate) && theUpdate >=1 && theUpdate <=7)
                    {
                        switch (theUpdate)
                        {
                            case 1:
                                theupdatedValue = Pet.Species.Dog;
                                break;
                            case 2:
                                theupdatedValue = Pet.Species.Cat;
                                break;
                            case 3:
                                theupdatedValue = Pet.Species.Fish;
                                break;
                            case 4:
                                theupdatedValue = Pet.Species.Horse;
                                break;
                            case 5:
                                theupdatedValue = Pet.Species.Hamster;
                                break;
                            case 6:
                                theupdatedValue = Pet.Species.Gerbil;
                                break;
                            case 7:
                                theupdatedValue = Pet.Species.Rabbit;
                                break;
                            default:
                                throw new InvalidDataException(message: "Index for species is out of bounds");
                        }
                    }
                    return _petRepo.UpdateSpeciesOfPet(updatedPet, theupdatedValue);

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
                        return _petRepo.UpdateOwnerOfPet(updatedPet, id);
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
            if (thePets.Count <= 0 || thePets.Count > 1)
            {
                throw new Exception(message: "I am sorry wrong amonut of pets found by ID.");
            }
            else
            {
                return _petRepo.UpdateFullPet(thePets[0], thePet);
            }
        }
    }
}
