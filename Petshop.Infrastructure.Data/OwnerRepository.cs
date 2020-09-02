using Petshop.Core.DomainService;
using Petshop.Core.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Petshop.Infrastructure.Data
{
    public class OwnerRepository: IOwnerRepository
    {
        
        public IEnumerable<Owner> GetAllOwners()
        {
            return PetDB.allTheOwners;
        }

        public IEnumerable<Owner> FindOwnerByName(string searchValue)
        {
            IEnumerable<Owner> ownerByName = PetDB.allTheOwners.Where(owner => owner.OwnerFirstName.Contains(searchValue) || owner.OwnerLastName.Contains(searchValue));
            return ownerByName;
        }

        public IEnumerable<Owner> FindOwnerByPhonenr(string searchValue)
        {
            IEnumerable<Owner> ownerByPhone = PetDB.allTheOwners.Where(owner => owner.OwnerPhoneNr.Contains(searchValue));
            return ownerByPhone;
        }

        public IEnumerable<Owner> FindOwnerByAddress(string searchValue)
        {
            IEnumerable<Owner> ownerByAddress = PetDB.allTheOwners.Where(owner => owner.OwnerAddress.Contains(searchValue));
            return ownerByAddress;
        }

        public IEnumerable<Owner> FindOwnerByEmail(string searchValue)
        {
            IEnumerable<Owner> ownerByEmail = PetDB.allTheOwners.Where(owner => owner.OwnerEmail.Contains(searchValue));
            return ownerByEmail;
        }

        public Owner FindOwnerByID(int searchId)
        {
            List<Owner> foundOwners = (PetDB.allTheOwners.Where(owner => owner.OwnerId == searchId)).ToList();
            if (foundOwners.Count <= 0 || foundOwners.Count > 1)
            {
                throw new Exception(message: "I am sorry wrong amonut of pets found by ID.");
            }
            else
            {
                return foundOwners[0];
            }
        }

        public Owner AddNewOwner(Owner theNewOwner)
        {
            return PetDB.addNewOwner(theNewOwner);
        }

        public Owner UpdateFirstNameOfOwner(Owner updatedOwner, string updateValue)
        {
            return PetDB.updateOwnerFirstName(updatedOwner, updateValue);
        }

        public Owner UpdateLastNameOfOwner(Owner updatedOwner, string updateValue)
        {
            return PetDB.updateOwnerLastName(updatedOwner, updateValue);
        }

        public Owner UpdateAddressOfOwner(Owner updatedOwner, string updateValue)
        {
            return PetDB.updateOwnerAddress(updatedOwner, updateValue);
        }

        public Owner UpdatePhoneNrOfOwner(Owner updatedOwner, string updateValue)
        {
            return PetDB.updateOwnerPhoneNr(updatedOwner, updateValue);
        }

        public Owner UpdateEmailOfOwner(Owner updatedOwner, string updateValue)
        {
            return PetDB.updateOwnerEmail(updatedOwner, updateValue);
        }

        public Owner DeleteOwnerById(int theId)
        {
            return PetDB.DeleteOwnerById(theId);
        }

        public IEnumerable<Pet> FindAllPetsByOwner(Owner theOwner)
        {
            IEnumerable<Pet> petsByOwner = PetDB.allThePets.Where(pet => pet.PetOwner == theOwner);
            return petsByOwner;
        }
    }
}
