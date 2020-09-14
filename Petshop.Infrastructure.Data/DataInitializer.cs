using Petshop.Core.DomainService;
using Petshop.Core.Enteties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Petshop.Infrastructure.Data
{
    public class DataInitializer
    {
        private readonly IOwnerRepository _ownerRepo;
        private readonly IPetRepository _petRepo;
        private readonly IPetTypeRepository _petTypeRepo;
        public DataInitializer(IOwnerRepository ownerRepository, IPetRepository petRepository, IPetTypeRepository petTypeRepository)
        {
            _ownerRepo = ownerRepository;
            _petRepo = petRepository;
            _petTypeRepo = petTypeRepository;
        }

        public string InitData()
        {
            List<PetType> allPetTypes = new List<PetType>
            {
                new PetType {PetTypeName = "Cat"},
                new PetType {PetTypeName = "Dog"},
                new PetType {PetTypeName = "Horse"},
                new PetType {PetTypeName = "Fish"},
                new PetType {PetTypeName = "Gerbil"},
                new PetType {PetTypeName = "Hamster"},
                new PetType {PetTypeName = "Rabbit"}
            };

            List<Owner> allOwners = new List<Owner>
            {
                new Owner{OwnerFirstName = "Lars", OwnerLastName = "Rasmussen", OwnerAddress = "SweetStreet 4, 6700 Esbjerg", OwnerPhoneNr = "+45 1234 5678", OwnerEmail = "lars@rasmussen.dk"},
                new Owner{OwnerFirstName = "John", OwnerLastName = "Jackson", OwnerAddress = "The Alley 6, 6705 Esbjerg Ø", OwnerPhoneNr = "+45 2549 6254", OwnerEmail = "thesuper_awesome@hotmail.com"},
                new Owner{OwnerFirstName = "Maria", OwnerLastName = "Saunderson", OwnerAddress = "Kongensgade 33, 6700 Esbjerg", OwnerPhoneNr = "+45 8761 1624", OwnerEmail = "suuper_sexy88@gmail.com"},
                new Owner{OwnerFirstName = "Belinda", OwnerLastName = "Twain", OwnerAddress = "Nørregade 14, 6700 Esbjerg", OwnerPhoneNr = "+45 7365 5976", OwnerEmail = "blender_wizard@hotmail.com"},
                new Owner{OwnerFirstName = "Roald", OwnerLastName = "Schwartz", OwnerAddress = "Lark Road 26, 6715 Esbjerg N", OwnerPhoneNr = "+45 7618 5234", OwnerEmail = "the_cool_roald@msnmail.com"},
                new Owner{OwnerFirstName = "Shiela", OwnerLastName = "Jesperson", OwnerAddress = "Daniels Road 45, 6700 Esbjerg", OwnerPhoneNr = "+45 7831 2561", OwnerEmail = "shiela45@gmail.com"},
                new Owner{OwnerFirstName = "Hansi", OwnerLastName = "Thompson", OwnerAddress = "Spooky Road 666, 6705 Esbjerg Ø", OwnerPhoneNr = "+45 1465 2845", OwnerEmail = "theghost83@outlook.com"},
                new Owner{OwnerFirstName = "Victoria", OwnerLastName = "Marks", OwnerAddress = "Birkelunden 8, 6705 Esbjerg Ø", OwnerPhoneNr = "+45 5956 4651", OwnerEmail = "vicmarks@hotmail.com"},
                new Owner{OwnerFirstName = "Niels", OwnerLastName = "Billson", OwnerAddress = "Folevej 3, 6715 Esbjerg N", OwnerPhoneNr = "+45 7286 9435", OwnerEmail = "ne49billson@gmail.com"},
                new Owner{OwnerFirstName = "Emanuelle", OwnerLastName = "Johnson", OwnerAddress = "Foldgårdsvej 17, 6715 Esbjerg N", OwnerPhoneNr = "+45 7315 4255", OwnerEmail = "emanuelle-actor@outlook.com"}
            };

            List<Pet>allPets = new List<Pet> {
                new Pet {PetBirthday = DateTime.Now.AddDays(-25), PetColor = "grey", PetName = "Hans", PetPreviousOwner = "Aniyah Chan", PetOwner= allOwners[0], PetSoldDate = DateTime.Now.AddMonths(0), PetType = allPetTypes[0], PetPrice = 10 },
                new Pet {PetBirthday = DateTime.Now.AddDays(-400), PetColor = "black and white", PetName = "Katia", PetPreviousOwner = "Alison Melia", PetOwner= allOwners[1], PetSoldDate = DateTime.Now.AddMonths(-3), PetType = allPetTypes[1], PetPrice = 235 },
                new Pet {PetBirthday = DateTime.Now.AddDays(-320), PetColor = "brown", PetName = "Jellybelly", PetPreviousOwner = "Abdallah Dejesus", PetOwner= allOwners[2], PetSoldDate = DateTime.Now.AddMonths(-5), PetType = allPetTypes[2], PetPrice = 2 },
                new Pet {PetBirthday = DateTime.Now.AddDays(-50), PetColor = "black", PetName = "Faithful", PetPreviousOwner = "Teegan Boyer", PetOwner= allOwners[3], PetSoldDate = DateTime.Now.AddMonths(-1), PetType = allPetTypes[1], PetPrice = 41 },
                new Pet {PetBirthday = DateTime.Now.AddDays(-81), PetColor = "orange striped", PetName = "Enigma", PetPreviousOwner = "Vinnie Odling", PetOwner= allOwners[4], PetSoldDate = DateTime.Now.AddMonths(-2), PetType = allPetTypes[3], PetPrice = 56 },
                new Pet {PetBirthday = DateTime.Now.AddDays(-691), PetColor = "purple", PetName = "Bob", PetPreviousOwner = "Amina Brookes", PetOwner= allOwners[4], PetSoldDate = DateTime.Now.AddMonths(-8), PetType = allPetTypes[3], PetPrice = 98 },
                new Pet {PetBirthday = DateTime.Now.AddDays(-259), PetColor = "silver tabby", PetName = "Linea", PetPreviousOwner = "Carmel Livingson", PetOwner= allOwners[5], PetSoldDate = DateTime.Now.AddMonths(-3), PetType = allPetTypes[4], PetPrice = 59 },
                new Pet {PetBirthday = DateTime.Now.AddDays(-856), PetColor = "caramel", PetName = "Tommy", PetPreviousOwner = "Nicole Jaramillo", PetOwner= allOwners[6], PetSoldDate = DateTime.Now.AddMonths(-15), PetType = allPetTypes[5], PetPrice = 76 },
                new Pet {PetBirthday = DateTime.Now.AddDays(-1576), PetColor = "black", PetName = "Beauty", PetPreviousOwner = "Hibah Bartlet", PetOwner= allOwners[7], PetSoldDate = DateTime.Now.AddMonths(-21), PetType = allPetTypes[6], PetPrice = 1090 },
                new Pet {PetBirthday = DateTime.Now.AddDays(-10), PetColor = "white", PetName = "Beatrice", PetPreviousOwner = "Radhika Baird", PetOwner= allOwners[8], PetSoldDate = DateTime.Now, PetType = allPetTypes[3], PetPrice = 28 },
                new Pet {PetBirthday = DateTime.Now.AddDays(-33), PetColor = "beige", PetName = "Jumpy", PetPreviousOwner = "Havin Boyle", PetOwner= allOwners[0], PetSoldDate = DateTime.Now.AddMonths(-1), PetType = allPetTypes[2], PetPrice = 100 },
                new Pet {PetBirthday = DateTime.Now.AddDays(-63), PetColor = "spotted brown", PetName = "Cujo", PetPreviousOwner = "Franklin Barajas", PetOwner= allOwners[9], PetSoldDate = DateTime.Now.AddMonths(-1), PetType = allPetTypes[6], PetPrice = 346 },
                new Pet {PetBirthday = DateTime.Now.AddDays(-18), PetColor = "merle", PetName = "Shenna", PetPreviousOwner = "Jovan Bloggs", PetOwner= allOwners[1], PetSoldDate = DateTime.Now, PetType = allPetTypes[3], PetPrice = 865 },
                new Pet {PetBirthday = DateTime.Now.AddDays(-156), PetColor = "red", PetName = "Firehoof", PetPreviousOwner = "Aamna Atherton", PetOwner= allOwners[7], PetSoldDate = DateTime.Now.AddMonths(-3), PetType = allPetTypes[0], PetPrice = 2096 }

            };

            foreach(var petType in allPetTypes)
            {
                _petTypeRepo.AddNewPetType(petType);
            }

            foreach (var owner in allOwners)
            {
                _ownerRepo.AddNewOwner(owner);
            }

            foreach (var pet in allPets)
            {
                _petRepo.AddNewPet(pet);
            }

            return "Fake data injected.";
        }
    }
}
