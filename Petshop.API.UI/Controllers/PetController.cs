using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Petshop.Core.ApplicationService;
using Petshop.Core.Enteties;

namespace Petshop.RestAPI.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetController : ControllerBase
    {
        private readonly IPetService _petService;
        private readonly IOwnerService _ownerService;
        public PetController (IPetService petService, IOwnerService ownerService)
        {
            _petService = petService;
            _ownerService = ownerService;
        }
   
        /*private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<PetController> _logger;

        public PetController(ILogger<PetController> logger)
        {
            _logger = logger;
        }*/

        [HttpGet]
        public IEnumerable<Pet> Get()
        {
            return _petService.GetAllPets();
        }

        [HttpGet("{petId}")]
        public ActionResult<Pet> Get(int petId)
        {
            return _petService.FindPetByID(petId);
        }

        /*public class newPetObj
        {
            public string petName { get; set; }
            public int? petSpecies { get; set; }
            public string petColor { get; set; }
            public DateTime petBirthday { get; set; }
            public DateTime petSoldDate { get; set; }
            public string petPreviousOwner { get; set; }
            public long? petPrice { get; set; }
            public int? ownerId { get; set; }
        }

        [HttpPost]
        public ActionResult<Pet> Post([FromBody] newPetObj theObject)
        {
            if (string.IsNullOrEmpty(theObject.petName) || !theObject.petSpecies.HasValue || string.IsNullOrEmpty(theObject.petColor) || theObject.petBirthday == null || theObject.petSoldDate == null || string.IsNullOrEmpty(theObject.petPreviousOwner) || theObject.petPrice == null || theObject.ownerId == null)
            {
                return BadRequest("You have not entered all the required data");
            }
            Owner newOwner = _ownerService.FindOwnerByID(theObject.ownerId.Value);
            if(newOwner == null)
            {
                return NotFound("I could not find an owner with that Id");
            }
            
            return Ok(_petService.AddNewPet(theObject.petName, theObject.petSpecies.Value, theObject.petColor, theObject.petBirthday, theObject.petSoldDate, theObject.petPreviousOwner, theObject.petPrice.Value, newOwner));
        }*/

        [HttpPost]
        public ActionResult<Pet> Post([FromBody] Pet thePet)
        {
            if (string.IsNullOrEmpty(thePet.PetName) || string.IsNullOrEmpty(thePet.PetSpecies.ToString())|| string.IsNullOrEmpty(thePet.PetColor) || thePet.PetBirthday == null || thePet.PetSoldDate == null || string.IsNullOrEmpty(thePet.PetPreviousOwner) || thePet.PetOwner == null)
            {
                return BadRequest("You have not entered all the required Pet data");
            }
            Owner theOwner = thePet.PetOwner;
            if(theOwner.OwnerId == 0)
            {
                if (string.IsNullOrEmpty(theOwner.OwnerFirstName) || string.IsNullOrEmpty(theOwner.OwnerLastName) || string.IsNullOrEmpty(theOwner.OwnerAddress) || string.IsNullOrEmpty(theOwner.OwnerPhoneNr) || string.IsNullOrEmpty(theOwner.OwnerEmail))
                {
                    return BadRequest("You have not entered all the required Owner data.");
                }
                theOwner = _ownerService.AddNewOwner(theOwner.OwnerFirstName, theOwner.OwnerLastName, theOwner.OwnerAddress, theOwner.OwnerPhoneNr, theOwner.OwnerEmail);
            }
            else
            {
                theOwner = _ownerService.FindOwnerByID(theOwner.OwnerId);
                if (theOwner == null)
                {
                    return NotFound("I could not find an owner with that Id");
                }
            }
            

            return Ok(_petService.AddNewPet(thePet.PetName, (int)thePet.PetSpecies , thePet.PetColor, thePet.PetBirthday, thePet.PetSoldDate, thePet.PetPreviousOwner, thePet.PetPrice, theOwner.OwnerId));
        }

        public class updatePetObj
        {
            public  int? updateParam { get; set; }
            public string updateData { get; set; }
        }

        [HttpPut("{id}")]
        public ActionResult<Pet> Put(int id, [FromBody] updatePetObj thePetObj)
        {
            if(thePetObj.updateParam == null || string.IsNullOrEmpty(thePetObj.updateData))
            {
                return BadRequest("You have not entered all the correct data.");
            }
            Pet thePet = _petService.FindPetByID(id);

            return Ok(_petService.UpdatePet(thePet, thePetObj.updateParam.Value, thePetObj.updateData));
        }

        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            Pet PetToDelete = _petService.FindPetByID(id);
            if(PetToDelete == null)
            {
                return NotFound("I could not find a pet with that Id to delete.");
            }

            _petService.DeletePetByID(id);
            return Ok(PetToDelete.PetName + " pet has been deleted.");
        }
    }
}
