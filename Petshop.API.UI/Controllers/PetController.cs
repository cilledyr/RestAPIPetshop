using System;
using System.Collections.Generic;
using System.IO;
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


        [HttpGet]
        public IEnumerable<Pet> Get()
        {
            return _petService.GetAllPets();
        }

        [HttpGet("{petId}")]
        public ActionResult<Pet> Get(int petId)
        { 
            try
            {
                return _petService.FindPetByID(petId);
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
            
        }

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

        [HttpPut("{id}/param")]
        [ActionName("UpdatePetParam")]
        public ActionResult<Pet> Put(int id, [FromBody] updatePetObj thePetObj)
        {
            if(thePetObj.updateParam == null || string.IsNullOrEmpty(thePetObj.updateData))
            {
                return BadRequest("You have not entered all the correct data.");
            }
            try
            {
                Pet thePet = _petService.FindPetByID(id);
                try
                {
                    return Ok(_petService.UpdatePet(thePet, thePetObj.updateParam.Value, thePetObj.updateData));
                }
                catch (InvalidDataException e)
                {
                    return BadRequest(e.Message);
                }
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }

            
        }

       
        [HttpPut("{id}")]
        [ActionName("UpdateFullPet")]
        public ActionResult<Pet> Put(int id, [FromBody] Pet theUpdatedPet)
        {
            if(theUpdatedPet.PetId != id)
            {
                return BadRequest("The id's of the Pet must match.");
            }
            if (string.IsNullOrEmpty(theUpdatedPet.PetName) || string.IsNullOrEmpty(theUpdatedPet.PetSpecies.ToString()) || string.IsNullOrEmpty(theUpdatedPet.PetColor) || theUpdatedPet.PetBirthday == null || theUpdatedPet.PetSoldDate == null || string.IsNullOrEmpty(theUpdatedPet.PetPreviousOwner) || theUpdatedPet.PetOwner == null)
            {
                return BadRequest("You have not entered all the required Pet data");
            }

            try
            {
                _petService.FindPetByID(id);
                return Ok(_petService.UpdatePet(theUpdatedPet));
            }
            catch(Exception e)
            {
                return NotFound(e);
            }
            
           
        }

        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            try
            {
                Pet PetToDelete = _petService.FindPetByID(id);
                _petService.DeletePetByID(id);
                return Ok(PetToDelete.PetName + " pet has been deleted.");
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }

        }
    }
}
