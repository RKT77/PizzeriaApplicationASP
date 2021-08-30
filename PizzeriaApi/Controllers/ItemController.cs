using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzeriaApplication.DTO;
using PizzeriaApplication.Exceptions;
using PizzeriaApplication.Helpers;
using PizzeriaApplication.ICommands.ICommandsItem;
using PizzeriaApplication.Requests;
using PizzeriaApplication.Searches;
using PizzeriaApi.DTO;
using PizzeriaApi.Helpers;

namespace PizzeriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        readonly IGetItem getItem;
        readonly IGetItems getItems;
        readonly IAddItem addItem;
        readonly IDeleteItem deleteItem;
        readonly IUpdateItem updateItem;

        public ItemController(IGetItem getItem, IGetItems getItems, IAddItem addItem, IDeleteItem deleteItem, IUpdateItem updateItem)
        {
            this.getItem = getItem;
            this.getItems = getItems;
            this.updateItem = updateItem;
            this.addItem = addItem;
            this.deleteItem = deleteItem;
            
        }
        // GET: api/Item
        /// <summary>
        /// Gets Items
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /RoleSearch
        ///     {
        ///        "IdItemType":1,
        ///        "IdOrder":1,
        ///        "Keyword":"word",
        ///        "Perpage":5,
        ///        "Pagenumber":1
        ///     }
        ///
        /// </remarks>
        /// <param name="value"></param>
        /// <returns>Roles</returns>
        /// <response code="201">Roles with the similar or same name</response>
        [HttpGet]
        public ActionResult<IEnumerable<ItemDTO>> Get([FromQuery] ItemSearch search)
        {
            try
            {
                return Ok(getItems.Execute(search).Data);
            }
            catch (NotFoundObjectException e)
            {
                return UnprocessableEntity(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
        
        /// <summary>
        /// Gets a Item
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Item/Id
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Item with the same id</returns>
        /// <response code="201">Returns the Item</response>
        /// <response code="400">If the Item is null</response>
        // GET: api/Item/5
        [HttpGet("{id}", Name = "GetItem")]
        public ActionResult<ItemDTO> Get(int id)
        {
            try
            {
                return Ok(getItem.Execute(id));
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

        // POST: api/Item
        /// <summary>
        /// Creates an Item.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post /ItemImageDTO
        ///     {
        ///        "Name": "Item35",
        ///        "ItemTypeId":1,
        ///        "Price":120,
        ///        "Image":Image
        ///     }
        ///
        /// </remarks>
        /// <param name="value"></param>
        /// <returns>A newly created Item</returns>
        /// <response code="201">Returns an Item</response>
        /// <response code="400">If the Item is null</response>
        [LoggedIn]
        [HttpPost]
        public ActionResult<ItemDTO> Post([FromForm] ItemImageDTO p)
        {
            var ext = Path.GetExtension(p.Image.FileName); //.gif

            if (!FileUpload.AllowedExtensions.Contains(ext))
            {
                return UnprocessableEntity("Image extension is not allowed.");
            }

            try
            {
                var newFileName = Guid.NewGuid().ToString() + "_" + p.Image.FileName;

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", newFileName);

                p.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                
            
            try
            {
                    var ItemRequest = new ItemRequest
                    {
                        Image=newFileName,
                        ItemTypeId=p.ItemTypeId,
                        Name=p.Name,
                        Price=p.Price
                    };
                var item = this.addItem.Execute(ItemRequest);
                return Created("api/Item/" + item.Id, item);
            }
            catch (ObjectDoesntExistException e)
            {
                return NotFound(e.Message);
            }
            catch (ObjectAlreadyExistsException e)
            {
                return UnprocessableEntity(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        // PUT: api/Item/5
        /// <summary>
        /// Updates a Item.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///      PUT /ItemRequest
        ///     {
        ///        "Name": "Item35",
        ///        "ItemTypeId":1,
        ///        "Price":120
        ///     }
        ///
        /// </remarks>
        /// <param name="value"></param>
        /// <returns>No Content</returns>
        /// <response code="204">Returns No Content</response>
        /// <response code="400">If the Name is already used</response>
        [LoggedIn]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ItemRequest value)
        {
            try
            {
                this.updateItem.Execute(value, id);
                return NoContent();
            }
            catch (ObjectDoesntExistException e)
            {
                return NotFound(e.Message);
            }
            catch (ObjectAlreadyExistsException e)
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
        /// Deletes a specific Item.
        /// </summary>
        /// <param name="id"></param>   
        [LoggedIn("Manager")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                this.deleteItem.Execute(id);
                return NoContent();
            }
            catch (ObjectDoesntExistException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
