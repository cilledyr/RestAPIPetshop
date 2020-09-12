﻿using System;
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
            return _ownerService.FindOwnerByID(id);
        }

        public class NewOwnerObject 
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Address { get; set; }
            public string PhoneNr { get; set; }
            public string Email { get; set; }

        }
        // POST api/<OwnerController>
        [HttpPost]
        public ActionResult<Owner> Post([FromBody] NewOwnerObject theOwner)
        {
            if(string.IsNullOrEmpty(theOwner.FirstName) || string.IsNullOrEmpty(theOwner.LastName) || string.IsNullOrEmpty(theOwner.Address) || string.IsNullOrEmpty(theOwner.PhoneNr) || string.IsNullOrEmpty(theOwner.Email))
            {
                return BadRequest("You have not entered all the needed data.");
            }
            return _ownerService.AddNewOwner(theOwner.FirstName, theOwner.LastName, theOwner.Address, theOwner.PhoneNr, theOwner.Email);
        }

        public class updateOwnerObj
        {
            public int? updateParam { get; set; }
            public string updateData { get; set; }
        }

        // PUT api/<OwnerController>/5
        [HttpPut("{id}")]
        public ActionResult<Owner> Put(int id, [FromBody] updateOwnerObj theOwnerObj)
        {
            Owner theOwner = _ownerService.FindOwnerByID(id);
            if (theOwner == null)
            {
                return NotFound("Could not find that owner to update.");
            }

            if (theOwnerObj.updateParam == null || string.IsNullOrEmpty(theOwnerObj.updateData))
            {
                return BadRequest("You have not entered the necessary data.");
            }
            
            return _ownerService.UpdateOwner(theOwner, theOwnerObj.updateParam.Value, theOwnerObj.updateData);
        }

        // DELETE api/<OwnerController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Owner toDelete = _ownerService.FindOwnerByID(id);
            if(toDelete == null)
            {
                return NotFound("Sorry could not find that owner to delete.");
            }
            _ownerService.DeleteOwnerByID(id);
            return Ok($"Owner with Id {id} deleted.");
        }
    }
}
