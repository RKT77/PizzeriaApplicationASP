using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzeriaApplication.DTO;
using PizzeriaApplication.Exceptions;
using PizzeriaApplication.ICommands.ICommandsItemType;
using PizzeriaApplication.Searches;
using PizzeriaApi.Helpers;

namespace PizzeriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemTypeController : ControllerBase
    {
        readonly IGetItemTypes getItemTypes;
        readonly IGetItemType getItemType;
        readonly IAddItemType addItemType;
        readonly IUpdateItemType updateItemType;
        readonly IDeleteItemType deleteItemType;

        public ItemTypeController(IGetItemTypes getItemTypes,IGetItemType getItemType,IAddItemType addItemType,IUpdateItemType updateItemType,IDeleteItemType deleteItemType)
        {
            this.getItemTypes = getItemTypes;
            this.getItemType = getItemType;
            this.addItemType = addItemType;
            this.updateItemType = updateItemType;
            this.deleteItemType = deleteItemType;
        }
        // GET: api/ItemType
        /// <summary>
        /// Gets ItemTypes
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /ItemTypeSearch
        ///     {
        ///        "Name":"Type"
        ///     }
        ///
        /// </remarks>
        /// <param name="value"></param>
        /// <returns>ItemTypes</returns>
        /// <response code="201">ItemTypes with the similar or same name</response>
        [HttpGet]
        public ActionResult<IEnumerable<ItemTypeDTO>> Get([FromQuery] ItemTypeSearch search)
        {
            try
            {
                return Ok(getItemTypes.Execute(search));
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        // GET: api/ItemType/5
        /// <summary>
        /// Gets a ItemType
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /ItemType/Id
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>ItemType with the same id</returns>
        /// <response code="201">Returns the ItemType</response>
        /// <response code="400">If the ItemType is null</response>
        [HttpGet("{id}", Name = "GetAT")]
        public ActionResult<ItemTypeDTO> Get(int id)
        {
            try
            {
                return Ok(getItemType.Execute(id));
            }
            catch (NotFoundObjectException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        // POST: api/ItemType
        /// <summary>
        /// Creates an ItemType.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post /ItemTypeDTO
        ///     {
        ///        "Name": "Ime tralala"
        ///     }
        ///
        /// </remarks>
        /// <param name="value"></param>
        /// <returns>A newly created ItemType</returns>
        /// <response code="204">Returns a ItemType</response>
        /// <response code="400">If the ItemType is null</response>
        [LoggedIn("Manager")]
        [HttpPost]
        public ActionResult<ItemTypeDTO> Post([FromBody] ItemTypeDTO value)
        {
            try
            {
                var obj=addItemType.Execute(value);
                return Created("api/ItemType", obj);
            }
            catch(ObjectAlreadyExistsException e)
            {
                return UnprocessableEntity(e.Message);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // PUT: api/ItemType/5
        /// <summary>
        /// Updates a ItemType.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /ItemTypeDTO
        ///     {
        ///        "Name": "Name"
        ///     }
        ///
        /// </remarks>
        /// <param name="value"></param>
        /// <returns>No Content</returns>
        /// <response code="204">Returns No Content</response>
        /// <response code="400">If the Name is already used</response>
        [LoggedIn("Manager")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ItemTypeDTO value)
        {
            try
            {
                this.updateItemType.Execute(value, id);
                return NoContent();
            }
            catch(ObjectDoesntExistException e)
            {
                return NotFound(e.Message);
            }
            catch(ObjectAlreadyExistsException e)
            {
                return UnprocessableEntity(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// Deletes a specific ItemType.
        /// </summary>
        /// <param name="id"></param>   
        [LoggedIn("Manager")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                this.deleteItemType.Execute(id);
                return NoContent();
            }
            catch(ObjectDoesntExistException e)
            {
                return NotFound(e.Message);
            }
            catch(Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}
