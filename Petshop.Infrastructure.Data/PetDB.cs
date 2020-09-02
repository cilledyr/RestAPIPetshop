﻿using Petshop.Core.Enteties;
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
        public static int thePetCount { get; set; }
        public static int theOwnerCount { get; set; }

        
        

        internal static Owner updateOwnerLastName(Owner updatedOwner, string updateValue)
        {
            List<Owner> foundOwners = (allTheOwners.Where(owner => owner == updatedOwner)).ToList();
            if (foundOwners.Count <= 0 || foundOwners.Count > 1)
            {
                throw new InvalidDataException(message: "I am sorry wrong amonut of owners found");
            }
            else
            {
                foundOwners[0].OwnerLastName = updateValue;
                return foundOwners[0];
            }
        }

        internal static Owner updateOwnerAddress(Owner updatedOwner, string updateValue)
        {
            List<Owner> foundOwners = (allTheOwners.Where(owner => owner == updatedOwner)).ToList();
            if (foundOwners.Count <= 0 || foundOwners.Count > 1)
            {
                throw new InvalidDataException(message: "I am sorry wrong amonut of owners found");
            }
            else
            {
                foundOwners[0].OwnerAddress = updateValue;
                return foundOwners[0];
            }
        }

        internal static Owner DeleteOwnerById(int theId)
        {
            List<Owner> deletedOwner = (allTheOwners.Where(owner => owner.OwnerId == theId)).ToList();

            if (deletedOwner.Count == 1)
            {
                allThePets = allThePets.Where(pet => pet.PetOwner != deletedOwner[0]);
                allTheOwners = allTheOwners.Where(owner => owner != deletedOwner[0]);
                return deletedOwner[0];
            }
            else
            {
                throw new InvalidDataException(message: "Wrong number of the id have been found.");
            }
        }

        internal static Owner updateOwnerPhoneNr(Owner updatedOwner, string updateValue)
        {
            List<Owner> foundOwners = (allTheOwners.Where(owner => owner == updatedOwner)).ToList();
            if (foundOwners.Count <= 0 || foundOwners.Count > 1)
            {
                throw new InvalidDataException(message: "I am sorry wrong amonut of owners found");
            }
            else
            {
                foundOwners[0].OwnerPhoneNr = updateValue;
                return foundOwners[0];
            }
        }

        internal static Owner updateOwnerEmail(Owner updatedOwner, string updateValue)
        {
            List<Owner> foundOwners = (allTheOwners.Where(owner => owner == updatedOwner)).ToList();
            if (foundOwners.Count <= 0 || foundOwners.Count > 1)
            {
                throw new InvalidDataException(message: "I am sorry wrong amonut of owners found");
            }
            else
            {
                foundOwners[0].OwnerEmail = updateValue;
                return foundOwners[0];
            }
        }

        internal static Owner updateOwnerFirstName(Owner updatedOwner, string updateValue)
        {
            List<Owner> foundOwners = (allTheOwners.Where(owner => owner == updatedOwner)).ToList();
            if (foundOwners.Count <= 0 || foundOwners.Count > 1)
            {
                throw new InvalidDataException(message: "I am sorry wrong amonut of owners found");
            }
            else
            {
                foundOwners[0].OwnerFirstName = updateValue;
                return foundOwners[0];
            }
        }

        internal static Owner addNewOwner(Owner theNewOwner)
        {
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
            List<Pet> foundPets = (allThePets.Where(pet => pet == updatedPet)).ToList();
            if (foundPets.Count <= 0 || foundPets.Count > 1)
            {
                throw new InvalidDataException(message: "I am sorry wrong amonut of pets found");
            }
            else
            {
                foundPets[0].PetPreviousOwner = updateValue;
                return foundPets[0];
            }
        }

        internal static Pet UpdatePriceOfPet(Pet updatedPet, long updateValue)
        {
            List<Pet> foundPets = (allThePets.Where(pet => pet == updatedPet)).ToList();
            if (foundPets.Count <= 0 || foundPets.Count > 1)
            {
                throw new InvalidDataException(message: "I am sorry wrong amonut of pets found");
            }
            else
            {
                foundPets[0].PetPrice = updateValue;
                return foundPets[0];
            }
        }

        internal static Pet UpdateOwnerOfPet(Pet updatedPet, int ownerId)
        {
            List<Pet> foundPets = (allThePets.Where(pet => pet == updatedPet)).ToList();
            if (foundPets.Count != 1)
            {
                throw new InvalidDataException(message: "I am sorry wrong amonut of pets found");
            }
            else
            {
                List<Owner> theOwner = allTheOwners.Where(owner => owner.OwnerId == ownerId).ToList();
                if(theOwner.Count == 1)
                {
                    foundPets[0].PetOwner = theOwner[0];
                    return foundPets[0];
                }
                else
                {
                    throw new InvalidDataException(message: "I am sorry the owner id seems invalid.");
                }
                
            }
        }

        internal static Pet UpdateSoldDateOfPet(Pet updatedPet, DateTime updateValue)
        {
            List<Pet> foundPets = (allThePets.Where(pet => pet == updatedPet)).ToList();
            if (foundPets.Count <= 0 || foundPets.Count > 1)
            {
                throw new InvalidDataException(message: "I am sorry wrong amonut of pets found");
            }
            else
            {
                foundPets[0].PetSoldDate = updateValue;
                return foundPets[0];
            }
        }

        internal static Pet UpdateBirthdayOfPet(Pet updatedPet, DateTime updateValue)
        {
            List<Pet> foundPets = (allThePets.Where(pet => pet == updatedPet)).ToList();
            if (foundPets.Count <= 0 || foundPets.Count > 1)
            {
                throw new InvalidDataException(message: "I am sorry wrong amonut of pets found");
            }
            else
            {
                foundPets[0].PetBirthday = updateValue;
                return foundPets[0];
            }
        }

        internal static Pet UpdateSpeciesOfPet(Pet updatedPet, Pet.Species updateValue)
        {
            List<Pet> foundPets = (allThePets.Where(pet => pet == updatedPet)).ToList();
            if (foundPets.Count <= 0 || foundPets.Count > 1)
            {
                throw new InvalidDataException(message: "I am sorry wrong amonut of pets found");
            }
            else
            {
                foundPets[0].PetSpecies = updateValue;
                return foundPets[0];
            }
        }

        internal static Pet UpdateColourOfPet(Pet updatedPet, string updateValue)
        {
            List<Pet> foundPets = (allThePets.Where(pet => pet == updatedPet)).ToList();
            if (foundPets.Count <= 0 || foundPets.Count > 1)
            {
                throw new InvalidDataException(message: "I am sorry wrong amonut of pets found");
            }
            else
            {
                foundPets[0].PetColor = updateValue;
                return foundPets[0];
            }
        }

        internal static Pet UpdateNameOfPet(Pet updatedPet, string updateValue)
        {
            List<Pet> foundPets = (allThePets.Where(pet => pet == updatedPet)).ToList();
            if (foundPets.Count <= 0 || foundPets.Count > 1)
            {
                throw new InvalidDataException(message: "I am sorry wrong amonut of pets found");
            }
            else
            {
                foundPets[0].PetName = updateValue;
                return foundPets[0];
            }

        }

        internal static Pet DeletePetById(int theId)
        {
            List<Pet> deletedPets = (allThePets.Where(pet => pet.PetId == theId)).ToList();
            if(deletedPets.Count == 1)
            {
                allThePets = allThePets.Where(pet => pet != deletedPets[0]);
                return deletedPets[0];
            }
            else
            {
                throw new InvalidDataException(message: "Wrong amount of Id's located.");
            }
        }

        internal static Pet AddNewPet(Pet theNewPet)
        {
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
    }

   
}
