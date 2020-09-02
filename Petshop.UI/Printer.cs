using Petshop.Core.ApplicationService;
using Petshop.Core.Enteties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Petshop.UI
{
    public class Printer
    {
        private IPetService _petService;
        private IOwnerService _ownerService;
        
        public Printer (IPetService petService, IOwnerService ownerService)
        {
            _ownerService = ownerService;
            _petService = petService;
        }
        public static List<string> menuItems = new List<string>
        {
            "1: List All Pets",
            "2: Add a new pet",
            "3: Delete a pet",
            "4: Update an existing pet",
            "5: Sort pets by price",
            "6: Cheapest 5 pets",
            "7: Search all pets",
            "8: Go to Owner Menu",
            "0: Exit Program"
        };

        public static List<string> ownerMenuItems = new List<string>
        {
            "1: Search all owners",
            "2: List all owners",
            "3: Add new Owner",
            "4: Delete Owner and all his pets",
            "5: Edit existing owner",
            "6: Find pets by owner",
            "7: Back to Pet Menu",
            "0: Exit the program"
        };
        public void DisplayMenu(string userName)
        {
            Console.WriteLine("\n \n");
            Console.WriteLine($"Please make a selection from the menu, type the nr of the selection you want to make, {userName}");
            foreach (var menuItem in menuItems)
            {
                Console.WriteLine(menuItem);
            }
            Console.WriteLine("\n");
            var selection = Console.ReadLine();
            int menuItemSelected;
            if (int.TryParse(selection, out menuItemSelected))
            {
                switch (menuItemSelected)
                {
                    case 1:
                        ListAllPets(userName);
                        break;
                    case 2:
                        AddNewPet(userName);
                        break;
                    case 3:
                        DeletePet(userName);
                        break;
                    case 4:
                        UpdatePet(userName);
                        break;
                    case 5:
                        SortPets(userName);
                        break;
                    case 6:
                        Find5Cheapest(userName);
                        break;
                    case 7:
                        SearchPets(userName);
                        break;
                    case 8:
                        DisplayOwnerMenu(userName);
                        break;
                    case 0:
                        ExitProgram(userName);
                        break;

                    default:
                        Console.WriteLine($"Oops, you seem to have selected something invalid, please try again {userName}.");
                        DisplayMenu(userName);
                        break;
                }
            }
            else
            {
                Console.WriteLine("You entered a wrong menu selection, please try again.");
                DisplayMenu(userName);
            }
        }

        private void DisplayOwnerMenu(string userName)
        {
            Console.WriteLine("\n \n");
            Console.WriteLine($"Please make a selection from the menu, type the nr of the selection you want to make, {userName}");
            foreach (var menuItem in ownerMenuItems)
            {
                Console.WriteLine(menuItem);
            }
            Console.WriteLine("\n");
            var selection = Console.ReadLine();
            int menuItemSelected;
            if (int.TryParse(selection, out menuItemSelected))
            {
                switch (menuItemSelected)
                {
                    case 1:
                        SearchOwner(userName);
                        break;
                    case 2:
                        ListAllOwners(userName);
                        break;
                    case 3:
                        AddTheOwnerFromMenu(userName);
                        break;
                    case 4:
                        DeleteOwner(userName);
                        break;
                    case 5:
                        EditOwner(userName);
                        break;
                    case 6:
                        FindPetsByOwner(userName);
                        break;
                    case 7:
                        DisplayMenu(userName);
                        break;
                    case 0:
                        ExitProgram(userName);
                        break;

                    default:
                        Console.WriteLine($"Oops, you seem to have selected something invalid, please try again {userName}.");
                        DisplayOwnerMenu(userName);
                        break;
                }
            }
            else
            {
                Console.WriteLine("You entered a wrong menu selection, please try again.");
                DisplayOwnerMenu(userName);
            }
        }

        private void PrintOwner(Owner owner)
        {
            Console.WriteLine($"Owner Id: {owner.OwnerId},\n Name: {owner.OwnerFirstName} {owner.OwnerLastName},\n Address: {owner.OwnerAddress},\n Phonenr: {owner.OwnerPhoneNr},\n Email: {owner.OwnerEmail}\n");
        }

        private void PrintPet(Pet pet)
        {
            Console.WriteLine($"Pet: ID: {pet.PetId},\n Species: {pet.PetSpecies},\n Name: {pet.PetName},\n Colour: {pet.PetColor},\n Birthday: {pet.PetBirthday},\n Owner: {pet.PetOwner.OwnerFirstName} {pet.PetOwner.OwnerLastName},\n Previous Owner: {pet.PetPreviousOwner},\n PurchaseDate: {pet.PetSoldDate},\n Price: {pet.PetPrice}£\n");
        }
        private Owner FindOwnerByNameOrID(string userName)
        {
            var theName = Console.ReadLine();
            int theId;
            Owner lookingFor = null;
            if (int.TryParse(theName, out theId))
            {
                lookingFor = _ownerService.FindOwnerByID(theId);
            }
            else
            {
                List<Owner> theLookedForOwners = _ownerService.FindOwnersByName(theName);
                if (theLookedForOwners.Count <= 0)
                {
                    Console.WriteLine($"I am sorry {userName}, I could not find any owners with that name, please start over.");
                    DisplayMenu(userName);
                }
                else if (theLookedForOwners.Count == 1)
                {
                    lookingFor = theLookedForOwners[0];
                }
                else
                {
                    Console.WriteLine($"I am sorry {userName} i have found {theLookedForOwners.Count} Owners by that name:");
                    foreach (var owner in theLookedForOwners)
                    {
                        PrintOwner(owner);
                    }
                    Console.WriteLine($"Please enter the ID of the owner you need:");
                    if (int.TryParse(Console.ReadLine(), out theId))
                    {
                        lookingFor = _ownerService.FindOwnerByID(theId);
                    }
                    else
                    {
                        throw new InvalidDataException(message: "I am sorry i have found an error, could not find the owner.");
                    }

                }
            }
            if(lookingFor == null)
            {
                throw new InvalidDataException(message: "I am sorry i have found an error, could not find the owner.");
            }
            return lookingFor;
        }

        private Pet FindPetByNameOrId(string userName)
        {
            var theName = Console.ReadLine();
            int theId;
            Pet updatedPet = null;
            if (int.TryParse(theName, out theId))
            {
                updatedPet = _petService.FindPetByID(theId);
            }
            else
            {
                List<Pet> petsByName = _petService.FindPetsByName(theName);
                if (petsByName.Count <= 0)
                {
                    Console.WriteLine($"I am sorry {userName}, i couldn't find any pet by that name, please start over.");
                    DisplayMenu(userName);

                }
                else if (petsByName.Count == 1)
                {
                    updatedPet = _petService.FindPetByID(petsByName[0].PetId);
                }
                else
                {
                    Console.WriteLine($"I am sorry {userName}, there are {petsByName.Count} pets with that name: \n");

                    foreach (var pet in petsByName)
                    {
                        PrintPet(pet);

                    }

                    Console.WriteLine($"Please enter the ID of the pet you need:");
                    var newId = Console.ReadLine();
                    int theNewId;
                    if (int.TryParse(newId, out theNewId))
                    {
                        updatedPet = _petService.FindPetByID(theNewId);
                    }
                    else
                    {
                        throw new InvalidDataException(message: "I could not find a pet.");
                        
                    }
                }
            }
            if(updatedPet == null)
            {
                throw new InvalidDataException(message: "Pet not found.");
            }
            return updatedPet;
        }
        private void FindPetsByOwner(string userName)
        {
            Console.WriteLine($"Ok {userName}, please enter the name or the id of the owner whose pets you would like to see:");

            Owner lookingFor = FindOwnerByNameOrID(userName);
            List<Pet> allTheFoundPets = _ownerService.FindAllPetsByOwner(lookingFor);
            Console.WriteLine($"Here is your complete list of pets owned by {lookingFor.OwnerFirstName} {lookingFor.OwnerLastName}:");
            foreach ( var pet in allTheFoundPets)
            {
                Console.WriteLine($"Pet: ID: {pet.PetId},\n Species: {pet.PetSpecies},\n Name: {pet.PetName},\n Colour: {pet.PetColor},\n Birthday: {pet.PetBirthday},\n Previous Owner: {pet.PetPreviousOwner},\n PurchaseDate: {pet.PetSoldDate},\n Price: {pet.PetPrice}£\n");
                //Not using PrintPet(pet) as i don't want to display the current owner again.
            }
            
            DisplayOwnerMenu(userName);
            
        }

        private void DeleteOwner(string userName)
        {
            Console.WriteLine($"{userName}, please enter the Name, or ID of the owner you would like to delete: ");
            Owner deletedOwner = FindOwnerByNameOrID(userName);
            deletedOwner = _ownerService.DeleteOwnerByID(deletedOwner.OwnerId);
            Console.WriteLine($"{userName}, you have successfully deleted {deletedOwner.OwnerFirstName} {deletedOwner.OwnerLastName}, from your database.");
            DisplayOwnerMenu(userName);
        }

        private void EditOwner(string userName)
        {
            Console.WriteLine($"{userName}, please enter the Name, or ID of the owner you would like to update: ");
            
            Owner updatedOwner = FindOwnerByNameOrID(userName);
            
            Console.WriteLine($"{userName}, you are updating ID: {updatedOwner.OwnerId} byt the name {updatedOwner.OwnerFirstName} {updatedOwner.OwnerLastName}. What would you like to update, of the following, Please type the nr only.?");
            Console.WriteLine(" 1: First Name \n 2: LastName \n 3: Address \n 4: Phonenr \n 5: Email \n ");
            var toUpdate = Console.ReadLine();
            int toUpdateInt = 0;
            string updateValue;
            if (int.TryParse(toUpdate, out toUpdateInt) && toUpdateInt <= 5 && toUpdateInt > 0)
            {
                switch (toUpdateInt)
                {
                    case 1:
                        Console.WriteLine($"What would you like to update the First Name to be? Current first name is: {updatedOwner.OwnerFirstName}.");
                        break;
                    case 2:
                        Console.WriteLine($"What would you like to update the Last Name to be? Current last name is: {updatedOwner.OwnerLastName}.");
                        break;
                    case 3:
                        Console.WriteLine($"What would you like to update the Address to be? Current address is: {updatedOwner.OwnerAddress}");
                        break;
                    case 4:
                        Console.WriteLine($"What would you like to update the Phonenr to be? Current phonenr is: {updatedOwner.OwnerPhoneNr}.");
                        break;
                    case 5:
                        Console.WriteLine($"What would you like to update the Email to be? Current email is: {updatedOwner.OwnerEmail}.");
                        break;
                    default:
                        break;
                }   
                updateValue = Console.ReadLine();
                updatedOwner = _ownerService.UpdateOwner(updatedOwner, toUpdateInt, updateValue);
                
            }

            else
            {
                throw new InvalidDataException(message: "I am sorry you have not entered a nr.");
            }


            Console.WriteLine($"{userName}, you have successfully updated {updatedOwner.OwnerFirstName} {updatedOwner.OwnerLastName}, in your database.");
            DisplayOwnerMenu(userName);
        }

        private Owner AddNewOwner(string userName)
        {
            Console.WriteLine($"You are about to create a new owner {userName}, what is the first name of this owner?");
            var firstname = Console.ReadLine();
            Console.WriteLine($"Thank you {userName}, what is {firstname}'s last name?");
            var lastname = Console.ReadLine();
            Console.WriteLine($"Please enter {firstname}, {lastname}'s address:");
            var address = Console.ReadLine();
            Console.WriteLine($"We are almost done, please enter {firstname} {lastname}'s phonenr:");
            var phonenr = Console.ReadLine();
            Console.WriteLine($"Lastly we ´need {firstname} {lastname}'s email:");
            var email = Console.ReadLine();

            Owner theNewOwner = _ownerService.AddNewOwner(firstname, lastname, address, phonenr, email);
            return theNewOwner;
        }

        private void AddTheOwnerFromMenu(string userName)
        {
            Owner theNewOwner = AddNewOwner(userName);
            Console.WriteLine($"Nice {userName}, you have successfully added {theNewOwner.OwnerFirstName} {theNewOwner.OwnerLastName} to the Database.");

            DisplayOwnerMenu(userName);
        }

        private void ListAllOwners(string userName)
        {
            List<Owner> allOwners = _ownerService.GetAllOwners();
            Console.WriteLine($"Here is a list of all the owners currently registered.");
            foreach (var owner in allOwners)
            {
                PrintOwner(owner);
            }
            DisplayOwnerMenu(userName);
        }

        private void SearchOwner(string userName)
        {
            {
                Console.WriteLine($"{userName}, which of the following would you like to search?, of the following, Please type the nr only.?");
                Console.WriteLine(" 1: Name \n 2: Address \n 3: Phonenr \n 4: Email \n 5: Id \n");
                var toSearch = Console.ReadLine();
                int toSearchInt = 0;
                string searchValue;
                if (int.TryParse(toSearch, out toSearchInt) && toSearchInt <= 5 && toSearchInt > 0)
                {
                    Console.WriteLine("Please enter what to search for below:");
                    searchValue = Console.ReadLine();

                    List<Owner> searchedOwner = _ownerService.SearchForOwner(toSearchInt, searchValue);
                    Console.WriteLine($"Here is your result {userName}.");
                    foreach (var owner in searchedOwner)
                    {
                        PrintOwner(owner);
                    }
                    DisplayOwnerMenu(userName);
                }
                else
                {
                    Console.WriteLine($"I am sorry {userName}, it looks like i can't search for that, please try again.");
                    SearchOwner(userName);
                }
            }
        }

        private void ExitProgram(string userName)
        {
            Console.WriteLine($"Goodbye {userName}");
            Environment.Exit(0);
        }

        private void SearchPets(string userName)
        {
            Console.WriteLine($"{userName}, which of the following would you like to search?, of the following, Please type the nr only.?");
            Console.WriteLine(" 1: Name \n 2: Colour \n 3: Species \n 4: Birthday \n 5: Sold Date \n 6: PreviousOwner \n 7: Price \n 8: Id \n 9: Owner \n");
            var toSearch = Console.ReadLine();
            int toSearchInt = 0;
            string searchValue;
            if (int.TryParse(toSearch, out toSearchInt) && toSearchInt <= 9 && toSearchInt > 0)
            {
                if(toSearchInt == 3)
                {
                    Console.WriteLine($"Thank you {userName}, now please enter the species from one of the following, enter the nr only: \n");
                    int theSpeciesNr = 0;
                    foreach (var theSpecies in Enum.GetNames(typeof(Pet.Species)))
                    {
                        theSpeciesNr++;
                        Console.WriteLine(theSpeciesNr + ": " + theSpecies);
                    }
                }
                else if(toSearchInt == 9)
                {
                    Console.WriteLine($"Ok {userName}, sending you to search for a specific owner and their pets.");
                    FindPetsByOwner(userName);
                }
                else
                {
                    Console.WriteLine("Please enter what to search for below, if searching for a birthday or SoldDate, please use the format: dd-mm-yyyy, only the year will be used. \n Price searched for will be +- 10£, for price enter only a nr please.");
                }
                searchValue = Console.ReadLine();
                
                List<Pet> searchedPets = _petService.SearchForPet(toSearchInt, searchValue);
                Console.WriteLine($"Here is your result {userName}.");
                foreach (var pet in searchedPets)
                {
                    PrintPet(pet);
                }
                DisplayMenu(userName);
            }
            else
            {
                Console.WriteLine($"I am sorry {userName}, it looks like i can't search for that, please try again.");
                SearchPets(userName);
            }
        }

        private void Find5Cheapest(string userName)
        {
            Console.WriteLine($"Here you go {userName}, the list of the five cheapest pets currently: ");
            List<Pet> sortedPets = _petService.GetSortedPets();
            for(int i = 0; i<5; i++ )
            {
                Pet pet = sortedPets[i];
                PrintPet(pet);
            }
            DisplayMenu(userName);
        }

        private void SortPets(string userName)
        {
            List<Pet> sortedPets = _petService.GetSortedPets();
            Console.WriteLine($"Here is the list of pets sorted by price");
            foreach (var pet in sortedPets)
            {
                PrintPet(pet);
            }
            DisplayMenu(userName);
        }

        private void UpdatePet(string userName)
        {
            Console.WriteLine($"{userName}, please enter the Name, or ID of the pet you would like to update for: ");
            Pet updatedPet = FindPetByNameOrId(userName);
                
            Console.WriteLine($"{userName}, you are updating ID: {updatedPet.PetId} byt the name {updatedPet.PetName}. What would you like to update, of the following, Please type the nr only.?");
            Console.WriteLine(" 1: Name \n 2: Colour \n 3: Species \n 4: Birthday \n 5: Sold Date \n 6: Previous owner \n 7: Price \n 8: Owner \n");
            var toUpdate = Console.ReadLine();
            int toUpdateInt = 0;
            string updateValue;
            if(int.TryParse(toUpdate, out toUpdateInt) && toUpdateInt <= 8 && toUpdateInt > 0)
            {
                switch (toUpdateInt)
                {
                   case 1:
                       Console.WriteLine($"What would you like to update the Name to be? Current name is: {updatedPet.PetName}.");
                       break;
                   case 2:
                       Console.WriteLine($"What would you like to update the Coulour to be? Current colour is: {updatedPet.PetColor}.");
                       break;
                   case 3:
                       Console.WriteLine($"What would you like to update the Species to be? Current Species is: {updatedPet.PetSpecies}. Select a nr from the list please.");
                       int theSpeciesNr = 0;
                       foreach (var theSpecies in Enum.GetNames(typeof(Pet.Species)))
                       {
                           theSpeciesNr++;
                           Console.WriteLine(theSpeciesNr + ": " + theSpecies);
                       }
                       break;
                   case 4:
                       Console.WriteLine($"What would you like to update the Birthdate to be? Current birthday is: {updatedPet.PetBirthday}. Enter new value in the format dd-mm-yyyy.");
                       break;
                   case 5:
                       Console.WriteLine($"What would you like to update the Sold date  to be? Current sold date is: {updatedPet.PetSoldDate}.Enter new value in the format dd-mm-yyyy.");
                       break;
                   case 6:
                       Console.WriteLine($"What would you like to update the Previous owner to be? Current Previous owner is: {updatedPet.PetPreviousOwner}.");
                       break;
                   case 7:
                       Console.WriteLine($"What would you like to update the Price to be? Current Price is: {updatedPet.PetPrice}£. Please enter the price in £, nr only.");
                       break;
                   case 8:
                        Console.WriteLine($"Please enter the ID of the owner you would like to place this pet under, current owner id: {updatedPet.PetOwner.OwnerId}, name: {updatedPet.PetOwner.OwnerFirstName} {updatedPet.PetOwner.OwnerLastName}");
                        break;
                   default:
                        break;
                }
               updateValue = Console.ReadLine();
               updatedPet = _petService.UpdatePet(updatedPet, toUpdateInt, updateValue);
            
            }

            else
            {
                throw new InvalidDataException(message: "I am sorry you have not entered a valid nr.");
            }


            Console.WriteLine($"{userName}, you have successfully updated {updatedPet.PetName}, from your database.");
            DisplayMenu(userName);
        }

        private void DeletePet(string userName)
        {
            Console.WriteLine($"{userName}, please enter the Name, or ID of the pet you would like to delete: ");
            Pet deletedPet = FindPetByNameOrId(userName);
            deletedPet = _petService.DeletePetByID(deletedPet.PetId);
            Console.WriteLine($"{userName}, you have successfully deleted {deletedPet.PetName}, from your database.");
            DisplayMenu(userName);
        }

        private void AddNewPet(string userName)
        {
            Console.WriteLine($"Hi {userName}, please enter the name of the new pet:");
            var thePetName = Console.ReadLine();
            
            var selectedSpecies = SelectSpecies(userName);
            int theSelectedSpecies;
            if (!int.TryParse(selectedSpecies, out theSelectedSpecies))
            {
                Console.WriteLine("You didn't enter a Nr for species, starting over.");
                AddNewPet(userName);
            }

            Console.WriteLine($"{userName}, please enter the colour of the pet:");
            var theColour = Console.ReadLine();

            Console.WriteLine($"Please enter the date of the pet's birthday in the format dd-mm-yyyy, {userName}:");
            var selectedBirthday = Console.ReadLine();
            DateTime theSelectedBirthday;
            if (!DateTime.TryParse(selectedBirthday, out theSelectedBirthday))
            {
                Console.WriteLine("You didn't enter a propper date format, starting over.");
                AddNewPet(userName);
            }
            
            Console.WriteLine($"Please enter the date of purchase in the format dd-mm-yyyy, {userName}:");
            var selectedPurchaseDate = Console.ReadLine();
            DateTime theSelectedPurchaseDate;
            if (!DateTime.TryParse(selectedPurchaseDate, out theSelectedPurchaseDate))
            {
                Console.WriteLine("You didn't enter a propper date format, starting over.");
                AddNewPet(userName);
            }

            Console.WriteLine($"{userName}, please enter the name of the previous owner:");
            var thePreviousOwner = Console.ReadLine();

            Console.WriteLine($"{userName}, please enter the price of the pet in £, nr only:");
            var petPrice = Console.ReadLine();
            long thePetPrice;
            if(!long.TryParse(petPrice, out thePetPrice))
            {
                Console.WriteLine("You didn't enter a propper price, starting over.");
                AddNewPet(userName);
            }

            Console.WriteLine($"Almost done {userName}, please enter the name or ID of the owner who owns this pet, enter a negative numeric value to create a new owner.");
            //Can't use method FindOwnerByNameOrID as i need to be able to create a new one in the same menu.
            var ownerID = Console.ReadLine();
            int theOwnerID;
            Owner newOwner = null;
            if(int.TryParse(ownerID, out theOwnerID))
            {
                if(theOwnerID >= 0)
                {
                    newOwner = _ownerService.FindOwnerByID(theOwnerID);
                }
                else
                {
                    Console.WriteLine($"Creating a new owner now");
                    newOwner = AddNewOwner(userName);
                }
            }
            else
            {
                List<Owner> theLookedForOwners = _ownerService.FindOwnersByName(ownerID);
                if (theLookedForOwners.Count <= 0)
                {
                    Console.WriteLine($"I am sorry {userName}, I could not find any owners with that name, please start over.");
                    FindPetsByOwner(userName);
                }
                else if (theLookedForOwners.Count == 1)
                {
                    newOwner = theLookedForOwners[0];
                }
                else
                {
                    Console.WriteLine($"I am sorry {userName} i have found {theLookedForOwners.Count} Owners by that name:");
                    foreach (var owner in theLookedForOwners)
                    {
                        PrintOwner(owner);
                    }
                    Console.WriteLine($"Please enter the ID of the owner whose pets you want to see:");
                    if (int.TryParse(Console.ReadLine(), out theOwnerID))
                    {
                        newOwner = _ownerService.FindOwnerByID(theOwnerID);
                    }
                    else
                    {
                        Console.WriteLine($"You have not given me a valid ID, please try again.");
                        FindPetsByOwner(userName);
                    }

                }
            }
            if(newOwner == null)
            {
                Console.WriteLine("Something unexpected has gone wrong with the owner, starting over.");
                AddNewPet(userName);
            }
            Pet theNewPet = _petService.AddNewPet(thePetName, theSelectedSpecies, theColour, theSelectedBirthday, theSelectedPurchaseDate, thePreviousOwner, thePetPrice, newOwner);

            Console.WriteLine($"Congratulatons {userName}, you have successfully added {theNewPet.PetName}, with the ID: {theNewPet.PetId}. \n");
            DisplayMenu(userName);

        }

            private string SelectSpecies(string userName)
        {
            Console.WriteLine($"Thank you {userName}, now please enter the species from one of the following, enter the nr only: \n");
            int theSpeciesNr = 0;
            foreach (var theSpecies in Enum.GetNames(typeof(Pet.Species)))
            {
                theSpeciesNr++;
                Console.WriteLine(theSpeciesNr + ": " + theSpecies);
            }
            var selectedSpecies = Console.ReadLine();
            return selectedSpecies;
        }

        private void ListAllPets(string userName)
        {
            List<Pet> allPets = _petService.GetAllPets();
            Console.WriteLine($"Here are all the available pets, {userName}:\n");

            foreach (var pet in allPets)
            {
                PrintPet(pet);
            }
            DisplayMenu(userName);
        }

    }
}
