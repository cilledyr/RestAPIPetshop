using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop.Core.Enteties
{
    
    public class Pet
    {
        public int PetId { get; set; }
        public string PetName { get; set; }

        public PetType PetType { get; set; }

        public DateTime PetBirthday { get; set; }
        public DateTime PetSoldDate { get; set; }
        public string PetColor { get; set; }
        public Owner PetOwner { get; set; }
        public string PetPreviousOwner { get; set; }
        public double PetPrice { get; set; }

    }
}
