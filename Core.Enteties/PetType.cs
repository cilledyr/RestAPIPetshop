using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop.Core.Enteties
{
    public class PetType
    {
        public int PetTypeId { get; set; }
        public string PetTypeName { get; set; }
        public List<Pet> PetTypePets { get; set; }
    }
}
