using Petshop.Core.DomainService;
using Petshop.Core.Enteties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Petshop.Core.ApplicationService.Impl
{
    public class OwnerService: IOwnerService
    {
        private IOwnerRepository _ownerRepo;
        public OwnerService (IOwnerRepository ownerRepository)
        {
            _ownerRepo = ownerRepository;
        }
        public Owner AddNewOwner(Owner theNewOwner)
        {
            return _ownerRepo.AddNewOwner(theNewOwner);
        }
        public Owner DeleteOwnerByID(int theId)
        {
            List<Owner> toBeDeletedOwners = _ownerRepo.FindOwner(theId);
            if(toBeDeletedOwners.Count != 1)
            {
                throw new Exception("Could not find anyone to delete.");
            }
            else
            {
                return _ownerRepo.DeleteOwner(toBeDeletedOwners[0]);
            }
        }
        public List<Pet> FindAllPetsByOwner(Owner theOwner)
        {
            return _ownerRepo.FindAllPetsByOwner(theOwner);
        }

        public Owner FindOwnerByID(int theId)
        {
            List<Owner> foundOwners = _ownerRepo.FindOwnerByID(theId);
            if (foundOwners.Count != 1)
            {
                return null;
            }
            else
            {
                Owner theOwner = foundOwners.Select(o => new Owner()
                {
                    OwnerId = o.OwnerId,
                    OwnerFirstName = o.OwnerFirstName,
                    OwnerLastName = o.OwnerLastName,
                    OwnerAddress = o.OwnerAddress,
                    OwnerPhoneNr = o.OwnerPhoneNr,
                    OwnerEmail = o.OwnerEmail,
                    OwnerPets = FindAllPetsByOwner(o)

                }).FirstOrDefault(o => o.OwnerId == theId);

                return theOwner;
            }
        }

        public List<Owner> FindOwnersByName(string theName)
        {
            return _ownerRepo.FindOwnerByName(theName).ToList();
        }
        public List<Owner> GetAllOwners()
        {
            return _ownerRepo.GetAllOwners().ToList();

        }

        public List<Owner> SearchForOwner(FilterModel filter)
        {
            string searchValue = filter.SearchValue;
            string searchTerm = filter.SearchTerm.ToLower();
            switch (searchTerm)
            {
                case "name":
                    return _ownerRepo.FindOwnerByName(searchValue).ToList();
                case "address":
                    return _ownerRepo.FindOwnerByAddress(searchValue).ToList();
                case "phonenr":
                    return _ownerRepo.FindOwnerByPhonenr(searchValue).ToList();

                case "email":
                    return _ownerRepo.FindOwnerByEmail(searchValue).ToList();

                case "id":
                    int searchId;
                    if (int.TryParse(searchValue, out searchId))
                    {
                        return _ownerRepo.FindOwner(searchId);
                    }
                    else
                    {
                        throw new InvalidDataException(message: "You have not given me a Nr to search the Id's for.");
                    }
                default:
                    throw new InvalidDataException(message: "I can't recognize that property to search for.");
            }
        }
        public Owner UpdateOwner(int updatedId, UpdateModel update)
        {
            int updateParam = update.UpdateParam.Value;
            string updateValue = update.UpdateValue;
            Owner updatedOwner = null;
            List<Owner> theOwners = _ownerRepo.FindOwner(updatedId);
            if(theOwners.Count != 1)
            {
                throw new Exception(message: "I am sorry, wrong id.");
            }
            else
            {
                updatedOwner = theOwners[0];
            }

            switch (updateParam)
            {
                case 1:
                    return _ownerRepo.UpdateFirstNameOfOwner(updatedOwner, updateValue);
                case 2:
                    return _ownerRepo.UpdateLastNameOfOwner(updatedOwner, updateValue);
                case 3:
                    return _ownerRepo.UpdateAddressOfOwner(updatedOwner, updateValue);
                case 4:
                    return _ownerRepo.UpdatePhoneNrOfOwner(updatedOwner, updateValue);
                case 5:
                    return _ownerRepo.UpdateEmailOfOwner(updatedOwner, updateValue);
                default:
                    throw new InvalidDataException(message: "I am sorry, i do not recognize what to update.");
            }
        }

        public Owner UpdateOwner(Owner theNewOwner)
        {
            
            List<Owner> theOwners = _ownerRepo.FindOwner(theNewOwner.OwnerId);
            if (theOwners.Count != 1)
            {
                throw new Exception(message: "I am sorry, wrong id.");
            }
            else
            {
                Owner theOldOwner = theOwners[0];
                return _ownerRepo.UpdateFullOwner(theNewOwner, theOldOwner);
            }
            
        }
    }
}
