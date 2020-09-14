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
        public IEnumerable<Owner> Get()
        {
            return _ownerService.GetAllOwners();
        }

        // GET api/<OwnerController>/5
        [HttpGet("{id}")]
        public ActionResult<Owner> Get(int id)
        {
            try
            {
                return Ok(_ownerService.FindOwnerByID(id));
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
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
            return _ownerService.AddNewOwner(theOwner.OwnerFirstName, theOwner.OwnerLastName, theOwner.OwnerAddress, theOwner.OwnerPhoneNr, theOwner.OwnerEmail);
        }

        public class updateOwnerObj
        {
            public int? updateParam { get; set; }
            public string updateData { get; set; }
        }
        // PUT api/<OwnerController>/5
        [HttpPut("{id}")]
        public ActionResult<Owner> Put(int id, [FromBody] Owner theOwner)
        { 
            if(id != theOwner.OwnerId)
            {
                return BadRequest("Your Id's need to match.");
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
                return BadRequest(e.Message);
            }
        }
        // PUT api/<OwnerController>/5/param
        [HttpPut("{id}/param")]
        public ActionResult<Owner> Put(int id, [FromBody] updateOwnerObj theOwnerObj)
        {
            try
            {
                return _ownerService.UpdateOwner(id, theOwnerObj.updateParam.Value, theOwnerObj.updateData);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/<OwnerController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _ownerService.FindOwnerByID(id);
                _ownerService.DeleteOwnerByID(id);
                return Ok($"Owner with Id {id} deleted.");
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
