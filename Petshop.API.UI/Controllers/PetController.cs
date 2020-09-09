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
   
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        /*private readonly ILogger<PetController> _logger;

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
        public Pet Get(int petId)
        {
            return _petService.FindPetByID(petId);
        }

        public class newPetObj
        {
            public string petName { get; set; }
            public int petSpecies { get; set; }
            public string petColor { get; set; }
            public DateTime petBirthday { get; set; }
            public DateTime petSoldDate { get; set; }
            public string petPreviousOwner { get; set; }
            public long petPrice { get; set; }
            public int ownerId { get; set; }
        }

        [HttpPost]
        public Pet Post([FromBody] newPetObj theObject)
        {
            Owner newOwner = _ownerService.FindOwnerByID(theObject.ownerId);
            return _petService.AddNewPet(theObject.petName, theObject.petSpecies, theObject.petColor, theObject.petBirthday, theObject.petSoldDate, theObject.petPreviousOwner, theObject.petPrice, newOwner);
        }

        public class updatePetObj
        {
            public  int updateParam { get; set; }
            public string updateData { get; set; }
        }

        [HttpPut("{id}")]
        public Pet Put(int id, [FromBody] updatePetObj thePetObj)
        {
            Pet thePet = _petService.FindPetByID(id);
            return _petService.UpdatePet(thePet, thePetObj.updateParam, thePetObj.updateData);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _petService.DeletePetByID(id);
        }
    }
}
