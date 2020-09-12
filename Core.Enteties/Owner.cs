using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop.Core.Enteties
{
    public class Owner
    {
        public int OwnerId { get; set; }
        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; }
        public string OwnerAddress { get; set; }
        public string  OwnerPhoneNr { get; set; }
        public string OwnerEmail { get; set; }
        public List<Pet> OwnerPets { get; set; }

        public Owner ()
        {
           
        }
    }
}
