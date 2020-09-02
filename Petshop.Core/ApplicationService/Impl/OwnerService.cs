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
        public Owner AddNewOwner(string firstname, string lastname, string address, string phonenr, string email)
        {
            Owner theNewOwner = new Owner();
            theNewOwner.OwnerFirstName = firstname;
            theNewOwner.OwnerLastName = lastname;
            theNewOwner.OwnerAddress = address;
            theNewOwner.OwnerPhoneNr = phonenr;
            theNewOwner.OwnerEmail = email;

            return _ownerRepo.AddNewOwner(theNewOwner);
        }
        public Owner DeleteOwnerByID(int theId)
        {
            return _ownerRepo.DeleteOwnerById(theId);
        }
        public List<Pet> FindAllPetsByOwner(Owner theOwner)
        {
            return _ownerRepo.FindAllPetsByOwner(theOwner).ToList();
        }

        public Owner FindOwnerByID(int theId)
        {
            return _ownerRepo.FindOwnerByID(theId);
        }

        public List<Owner> FindOwnersByName(string theName)
        {
            return _ownerRepo.FindOwnerByName(theName).ToList();
        }
        public List<Owner> GetAllOwners()
        {
            return _ownerRepo.GetAllOwners().ToList();
        }

        public List<Owner> SearchForOwner(int toSearchInt, string searchValue)
        {
            switch (toSearchInt)
            {
                case 1:
                    return _ownerRepo.FindOwnerByName(searchValue).ToList();
                case 2:
                    return _ownerRepo.FindOwnerByAddress(searchValue).ToList();
                case 3:
                    return _ownerRepo.FindOwnerByPhonenr(searchValue).ToList();

                case 4:
                    return _ownerRepo.FindOwnerByEmail(searchValue).ToList();

                case 5:
                    int searchId;
                    if (int.TryParse(searchValue, out searchId))
                    {
                        return new List<Owner> { _ownerRepo.FindOwnerByID(searchId) };
                    }
                    else
                    {
                        throw new InvalidDataException(message: "You have not given me a Nr to search the Id's for.");
                    }
                default:
                    throw new InvalidDataException(message: "Something unexpected went wrong.");
            }
        }
        public Owner UpdateOwner(Owner updatedOwner, int toUpdateInt, string updateValue)
        {
            switch (toUpdateInt)
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
                    throw new InvalidDataException(message: "Something unexpected went wrong.");
            }
        }
    }
}
