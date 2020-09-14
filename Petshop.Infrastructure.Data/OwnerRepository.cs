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

        public List<Owner> FindOwnerByID(int searchId)
        {
            return PetDB.allTheOwners.Where(owner => owner.OwnerId == searchId).ToList();
            
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

        public Owner DeleteOwner(Owner toBeDeletedOwner)
        {
            return PetDB.DeleteOwner(toBeDeletedOwner);
        }

        public List<Pet> FindAllPetsByOwner(Owner theOwner)
        {
            List<Pet> OwnerPets = PetDB.allThePets.Where(pet => pet.PetOwner == theOwner).ToList();
            return OwnerPets;
        }

        public List<Owner> FindOwner(int theOwnerId)
        {
            return PetDB.allTheOwners.Where(owner => owner.OwnerId == theOwnerId).ToList();
           
        }
        public Owner UpdateFullOwner(Owner theNewOwner, Owner theOldOwner)
        {
            return PetDB.UpdateFullOwner(theNewOwner, theOldOwner);
        }
    }
}
