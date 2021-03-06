﻿using Petshop.Core.Enteties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop.Core.DomainService
{
    public interface IOwnerRepository
    {
        public IEnumerable<Owner> FindOwnerByName(string searchValue);
        public IEnumerable<Owner> FindOwnerByPhonenr(string searchValue);
        public IEnumerable<Owner> FindOwnerByAddress(string searchValue);
        public IEnumerable<Owner> FindOwnerByEmail(string searchValue);
        public List<Owner> FindOwnerByID(int searchId);
        public Owner UpdateFirstNameOfOwner(Owner updatedOwner, string updateValue);
        public Owner UpdateLastNameOfOwner(Owner updatedOwner, string updateValue);
        public Owner UpdateAddressOfOwner(Owner updatedOwner, string updateValue);
        public Owner UpdatePhoneNrOfOwner(Owner updatedOwner, string updateValue);
        public Owner UpdateEmailOfOwner(Owner updatedOwner, string updateValue);
        public Owner DeleteOwner(Owner toBeDeletedOwner);
        public List<Pet> FindAllPetsByOwner(Owner theOwner);
        public IEnumerable<Owner> GetAllOwners();
        public Owner AddNewOwner(Owner theNewOwner);

        public List<Owner> FindOwner(int theOwnerId);
        public Owner UpdateFullOwner(Owner theNewOwner, Owner theOldOwner);
    }
}
