using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
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
        public ActionResult<IEnumerable<Pet>> Get([FromQuery] FilterModel filter)
        {
            if(string.IsNullOrEmpty(filter.SearchTerm) && string.IsNullOrEmpty(filter.SearchValue))
            {
                try
                {
                    return Ok(_petService.GetAllPets());
                }
                catch (Exception e)
                {
                    return NotFound(e.Message);
                }
            }
            else
            {
                if(string.IsNullOrEmpty(filter.SearchTerm) || string.IsNullOrEmpty(filter.SearchValue))
                {
                    return BadRequest("You need to enter both a SearchTerm and a SearchValue");
                }
                else
                {
                    try
                    {
                        return Ok(_petService.SearchForPet(filter));
                    }
                    catch (Exception e)
                    {
                        return NotFound(e.Message);
                    }
                }
                
            }
            
        }

        [HttpGet("{petId}")]
        public ActionResult<Pet> Get(int petId)
        { 
            try
            {
                return Ok(_petService.FindPetByID(petId));
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
            
        }

        [HttpPost]
        public ActionResult<Pet> Post([FromBody] Pet thePet)
        {
            if (string.IsNullOrEmpty(thePet.PetName) || thePet.PetType == null|| string.IsNullOrEmpty(thePet.PetColor) || thePet.PetBirthday == null || thePet.PetSoldDate == null || string.IsNullOrEmpty(thePet.PetPreviousOwner) || thePet.PetOwner == null)
            {
                return BadRequest("You have not entered all the required Pet data");
            }
            PetType thePetType = thePet.PetType;
            if(thePetType.PetTypeId == 0)
            {
                if(string.IsNullOrEmpty(thePetType.PetTypeName))
                {
                    return BadRequest("You have not entered all the information for a new PetType, please enter an id of an existing type, or a name for a new one.");
                }
            }

            Owner theOwner = thePet.PetOwner;
            if(theOwner.OwnerId == 0)
            {
                if (string.IsNullOrEmpty(theOwner.OwnerFirstName) || string.IsNullOrEmpty(theOwner.OwnerLastName) || string.IsNullOrEmpty(theOwner.OwnerAddress) || string.IsNullOrEmpty(theOwner.OwnerPhoneNr) || string.IsNullOrEmpty(theOwner.OwnerEmail))
                {
                    return BadRequest("You have not entered all the required Owner data, please enter the id of an existing owner, or all the info of a new one.");
                }
            }
            try
            {
                return Created("Successfully created the following pet: ", _petService.AddNewPet(thePet));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
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
                return Accepted("You successfully updated: ", _petService.UpdatePet(id, thePetObj.updateParam.Value, thePetObj.updateData));
                
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
            if (string.IsNullOrEmpty(theUpdatedPet.PetName) || theUpdatedPet.PetType == null || string.IsNullOrEmpty(theUpdatedPet.PetColor) ||
                theUpdatedPet.PetBirthday == null || theUpdatedPet.PetSoldDate == null || string.IsNullOrEmpty(theUpdatedPet.PetPreviousOwner) || theUpdatedPet.PetOwner == null ||
                theUpdatedPet.PetType == null || (theUpdatedPet.PetType.PetTypeId == 0 && string.IsNullOrEmpty(theUpdatedPet.PetType.PetTypeName)))
            {
                return BadRequest("You have not entered all the required Pet data");
            }

            try
            {
                return Accepted("You successfully updat4ed: ", _petService.UpdatePet(theUpdatedPet));
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
            
           
        }

        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            try
            {
                Pet PetToDelete = _petService.FindPetByID(id);
                _petService.DeletePetByID(id);
                return Accepted(PetToDelete.PetName + " pet has been deleted.");
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }

        }
    }
}
