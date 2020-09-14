using Petshop.Core.Enteties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Petshop.Infrastructure.Data
{
    public static class PetDB
    {
        public static IEnumerable<Pet> allThePets { get; set; }
        public static IEnumerable<Owner> allTheOwners { get; set; }

        public static IEnumerable<PetType> allThePetTypes { get; set; }
        public static int thePetCount { get; set; }
        public static int theOwnerCount { get; set; }
        public static int thePetTypeCount { get; set; }
        
        

        internal static Owner updateOwnerLastName(Owner updatedOwner, string updateValue)
        {
            updatedOwner.OwnerLastName = updateValue;
            return updatedOwner;
            
        }

        internal static Owner updateOwnerAddress(Owner updatedOwner, string updateValue)
        {
            updatedOwner.OwnerAddress = updateValue;
            return updatedOwner;
        }

        internal static Owner DeleteOwner(Owner toBeDeletedOwner)
        {
            allThePets = allThePets.Where(pet => pet.PetOwner != toBeDeletedOwner);
            allTheOwners = allTheOwners.Where(owner => owner != toBeDeletedOwner);
            return toBeDeletedOwner;
        }

        internal static Owner updateOwnerPhoneNr(Owner updatedOwner, string updateValue)
        {
            updatedOwner.OwnerPhoneNr = updateValue;
            return updatedOwner;
        }

        internal static Owner updateOwnerEmail(Owner updatedOwner, string updateValue)
        {
            updatedOwner.OwnerEmail = updateValue;
            return updatedOwner;
        }

        internal static Owner updateOwnerFirstName(Owner updatedOwner, string updateValue)
        {
            updatedOwner.OwnerFirstName = updateValue;
            return updatedOwner;
        }

        internal static Owner addNewOwner(Owner theNewOwner)
        {
            if(theOwnerCount == 0)
            {
                theOwnerCount++;
            }
            theNewOwner.OwnerId = theOwnerCount;
            theOwnerCount++;
            List<Owner> newOwner = new List<Owner> { theNewOwner };
            if(allTheOwners == null)
            {
                allTheOwners = newOwner;
            }
            else
            {
                allTheOwners = allTheOwners.Concat(newOwner);
            }
            
            return theNewOwner;
        }

        internal static Pet UpdatePreviousOwnerOfPet(Pet updatedPet, string updateValue)
        {
            updatedPet.PetPreviousOwner = updateValue;
            return updatedPet;
        }

        internal static Pet UpdatePriceOfPet(Pet updatedPet, long updateValue)
        {
            updatedPet.PetPrice = updateValue;
            return updatedPet;
        }

        internal static Pet UpdateOwnerOfPet(Pet updatedPet, Owner newOwner)
        {
            updatedPet.PetOwner = newOwner;
            return updatedPet;
        }

        internal static Pet UpdateSoldDateOfPet(Pet updatedPet, DateTime updateValue)
        {
            updatedPet.PetSoldDate = updateValue;
            return updatedPet;
        }

        internal static Pet UpdateBirthdayOfPet(Pet updatedPet, DateTime updateValue)
        {
            updatedPet.PetBirthday = updateValue;
            return updatedPet;
        }

        internal static Pet UpdateTypeOfPet(Pet updatedPet, PetType updateValue)
        {
            updatedPet.PetType = updateValue;
            return updatedPet;
        }

        internal static Pet UpdateColourOfPet(Pet updatedPet, string updateValue)
        {
            updatedPet.PetColor = updateValue;
            return updatedPet;
        }

        internal static Pet UpdateNameOfPet(Pet updatedPet, string updateValue)
        {
            updatedPet.PetName = updateValue;
            return updatedPet;
        }

        internal static Pet DeletePet(Pet toBeDeletedPet)
        {
            allThePets = allThePets.Where(pet => pet != toBeDeletedPet);
            return toBeDeletedPet;

        }

        internal static Pet UpdateFullPet(Pet theOldPet, Pet theNewPet)
        {
            theOldPet.PetName = theNewPet.PetName;
            theOldPet.PetType = theNewPet.PetType;
            theOldPet.PetBirthday = theNewPet.PetBirthday;
            theOldPet.PetSoldDate = theNewPet.PetSoldDate;
            theOldPet.PetColor = theNewPet.PetColor;
            theOldPet.PetOwner = theNewPet.PetOwner;
            theOldPet.PetPreviousOwner = theNewPet.PetPreviousOwner;
            theOldPet.PetPrice = theNewPet.PetPrice;
            return theOldPet;
        }

        internal static Pet AddNewPet(Pet theNewPet)
        {
            if(thePetCount == 0)
            {
                thePetCount++;
            }
            theNewPet.PetId = thePetCount;
            thePetCount++;
            List<Pet> newPet = new List<Pet> { theNewPet };
            if(allThePets == null)
            {
                allThePets = newPet;
            }
            else
            {
                allThePets = allThePets.Concat(newPet);
            }
            return theNewPet;
        }

        internal static Owner UpdateFullOwner(Owner theNewOwner, Owner theOldOwner)
        {
            theOldOwner.OwnerFirstName = theNewOwner.OwnerFirstName;
            theOldOwner.OwnerLastName = theNewOwner.OwnerLastName;
            theOldOwner.OwnerAddress = theNewOwner.OwnerAddress;
            theOldOwner.OwnerPhoneNr = theNewOwner.OwnerPhoneNr;
            theOldOwner.OwnerEmail = theNewOwner.OwnerEmail;
            return theOldOwner;
        }

        internal static PetType AddNewPetType(PetType theNewPetType)
        {
            if(thePetTypeCount == 0)
            {
                thePetTypeCount++;
            }
            theNewPetType.PetTypeId = thePetCount;
            thePetTypeCount++;
            List<PetType> newPetType = new List<PetType> { theNewPetType };
            if(allThePetTypes == null)
            {
                allThePetTypes = newPetType;
            }
            else
            {
                allThePetTypes = allThePetTypes.Concat(newPetType);
            }
            return theNewPetType;
        }

        internal static PetType DeletePetType(PetType toBeDeletedType)
        {
            allThePets = allThePets.Where(pet => pet.PetType != toBeDeletedType);
            allThePetTypes = allThePetTypes.Where(petType => petType != toBeDeletedType);
            return toBeDeletedType;
        }

        internal static PetType UpdatePetType(PetType theNewPetType, PetType theOldPetType)
        {
            theOldPetType.PetTypeName = theNewPetType.PetTypeName;
            return theOldPetType;
        }
    }

   
}
