using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Petshop.Core.ApplicationService;
using Petshop.Core.Enteties;


namespace Petshop.RestAPI.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetTypeController : ControllerBase
    {
        private readonly IPetTypeService _petTypeService;
        public PetTypeController(IPetTypeService petTypeService)
        {
            _petTypeService = petTypeService;
        }
        // GET: api/<PetTypeController>
        [HttpGet]
        public ActionResult<List<PetType>> Get([FromQuery] FilterModel filter)
        {
            if (string.IsNullOrEmpty(filter.SearchTerm) && string.IsNullOrEmpty(filter.SearchValue))
            {
                try
                {
                    List<PetType> allTheTypes = _petTypeService.GetALlPetTypes();
                    if(allTheTypes.Count <1)
                    {
                        return NotFound("I am sorry it seems there are no PetTypes.");
                    }
                    else
                    {
                        return Ok(allTheTypes);
                    }
                }
                catch (Exception e)
                {
                    return StatusCode(500, e.Message);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(filter.SearchTerm) || string.IsNullOrEmpty(filter.SearchValue))
                {
                    return BadRequest("You need to enter both a SearchTerm and a SearchValue");
                }
                else
                {
                    try
                    {
                        List<PetType> allTheTypes = _petTypeService.SearchPetType(filter);
                        if(allTheTypes.Count < 1)
                        {
                            return NotFound("I am sorry could not find any petTypes with those parameters.");
                        }
                        else
                        {
                            return Ok(allTheTypes);
                        }
                    }
                    catch (Exception e)
                    {
                        return StatusCode(500, e.Message);
                    }
                }
            }
        }

        // GET api/<PetTypeController>/5
        [HttpGet("{id}")]
        public ActionResult<PetType> Get(int id)
        {
            try
            {
                PetType theType = _petTypeService.FindPetTypeByIdWithPets(id);
                if(theType == null)
                {
                    return NotFound("I am sorry could not find a type with that Id.");
                }
                else
                {
                    return Ok(theType);
                }
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // POST api/<PetTypeController>
        [HttpPost]
        public ActionResult<PetType> Post([FromBody] PetType theNewType)
        {
            if (string.IsNullOrEmpty(theNewType.PetTypeName))
            {
                return BadRequest("You need to enter a name to create a new Type.");
            }
            else
            {
                try
                {
                    return Created("Successfully created the following petType", _petTypeService.AddNewPetType(theNewType));
                }
                catch (Exception e)
                {
                    return StatusCode(500, e.Message);
                }
            }
            
        }

        // PUT api/<PetTypeController>/5
        [HttpPut("{id}")]
        public ActionResult<PetType> Put(int id, [FromBody] PetType theUpdatedPetType)
        {
            if(id != theUpdatedPetType.PetTypeId || id == 0)
            {
                return BadRequest("The Id's must match, and may not be 0.");
            }
            else if(string.IsNullOrEmpty(theUpdatedPetType.PetTypeName))
            {
                return BadRequest("You need to enter a name for the new type.");
            }
            else
            {
                try
                {
                    return Accepted(_petTypeService.UpdatePetType(theUpdatedPetType));
                }
                catch (Exception e)
                {
                    return StatusCode(500, e.Message);
                }
            }
            
        }

        // DELETE api/<PetTypeController>/5
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            try
            {
                _petTypeService.DeletePetType(id);
                return Accepted($"Successfully deleted pet with the id {id}");
            }
            catch(Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
