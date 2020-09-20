using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Petshop.Core.ApplicationService;
using Petshop.Core.Enteties;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Petshop.RestAPI.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;
        private readonly IPetService _petService;

        public OwnerController(IPetService petService, IOwnerService ownerService)
        {
            _petService = petService;
            _ownerService = ownerService;
        }
        // GET: api/<OwnerController>
        [HttpGet]
        public ActionResult<IEnumerable<Owner>> Get([FromQuery] FilterModel filter)
        {
            if(string.IsNullOrEmpty(filter.SearchTerm) && string.IsNullOrEmpty(filter.SearchValue))
            {
                try
                {
                    List<Owner> alltheOwners = _ownerService.GetAllOwners();
                    if(alltheOwners.Count < 1)
                    {
                        return NotFound("I am sorry, it seems there are no owners.");
                    }
                    else
                    {
                        return Ok(alltheOwners);
                    }
                    
                }
                catch (Exception e)
                {
                    return StatusCode(500, e.Message);
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
                        List<Owner> allTheOwners = _ownerService.SearchForOwner(filter);
                        if (allTheOwners.Count < 1)
                        {
                            return NotFound("I am sorry i could not find any owners with those parameters.");
                        }
                        else
                        {
                            return Ok(allTheOwners);
                        }
                    }
                    catch(Exception e)
                    {
                        return StatusCode(500, e.Message);
                    }
                }
            }
        }

        // GET api/<OwnerController>/5
        [HttpGet("{id}")]
        public ActionResult<Owner> Get(int id)
        {
            try
            {
                Owner theOwner = _ownerService.FindOwnerByID(id);
                if(theOwner == null)
                {
                    return NotFound("I am sorry could not find an owner with that ID.");
                }
                else
                {
                    return Ok(theOwner);
                }
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

         // POST api/<OwnerController>
        [HttpPost]
        public ActionResult<Owner> Post([FromBody] Owner theOwner)
        {
            if(string.IsNullOrEmpty(theOwner.OwnerFirstName) || string.IsNullOrEmpty(theOwner.OwnerLastName) || string.IsNullOrEmpty(theOwner.OwnerAddress) || string.IsNullOrEmpty(theOwner.OwnerPhoneNr) || string.IsNullOrEmpty(theOwner.OwnerEmail))
            {
                return BadRequest("You have not entered all the needed data.");
            }
            try
            {
                return Created("Successfully created the following: ", _ownerService.AddNewOwner(theOwner));
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // PUT api/<OwnerController>/5
        [HttpPut("{id}")]
        public ActionResult<Owner> Put(int id, [FromBody] Owner theOwner)
        { 
            if(id != theOwner.OwnerId || id == 0)
            {
                return BadRequest("Your Id's need to match, and may not be 0.");
            }
            if (string.IsNullOrEmpty(theOwner.OwnerFirstName) || string.IsNullOrEmpty(theOwner.OwnerLastName) || string.IsNullOrEmpty(theOwner.OwnerAddress) || string.IsNullOrEmpty(theOwner.OwnerPhoneNr) || string.IsNullOrEmpty(theOwner.OwnerEmail))
            {
                return BadRequest("You have not entered all the needed data.");
            }
            try
            {
                return Ok(_ownerService.UpdateOwner(theOwner));
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        // PUT api/<OwnerController>/5/param
        [HttpPut("{id}/param")]
        public ActionResult<Owner> Put(int id, [FromBody] UpdateModel update)
        {
            if (update.UpdateParam == null || string.IsNullOrEmpty(update.UpdateValue))
            {
                return BadRequest( "You have not entered all the correct data.");
            }
            try
            {
                return _ownerService.UpdateOwner(id, update);
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // DELETE api/<OwnerController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _ownerService.DeleteOwnerByID(id);
                return Ok($"Owner with Id {id} deleted.");
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
