using Petshop.Core.Enteties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop.Core.ApplicationService
{
    public interface IOwnerService
    {
        public List<Owner> GetAllOwners();
        public List<Owner> SearchForOwner(FilterModel filter);
        public Owner AddNewOwner(Owner theNewOwner);
        public List<Owner> FindOwnersByName(string theName);
        public Owner FindOwnerByID(int theId);
        public Owner UpdateOwner(int ownerId, int toUpdateInt, string updateValue);
        public Owner DeleteOwnerByID(int theId);
        public List<Pet> FindAllPetsByOwner(Owner theOwner);
        public Owner UpdateOwner(Owner theOldOwner);
    }
}
