using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzeriaApplication.DTO;
using PizzeriaApplication.Exceptions;
using PizzeriaApplication.ICommands;
using PizzeriaApplication.Searches;
using PizzeriaApi.Helpers;

namespace PizzeriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzeriaHallController : ControllerBase
    {
        readonly IGetPizzeriaHalls getPizzeriaHalls;
        readonly IAddPizzeriaHall addPizzeriaHall;
        readonly IGetPizzeriaHall getPizzeriaHall;
        readonly IDeletePizzeriaHall deletePizzeriaHall;
        readonly IUpdatePizzeriaHall updatePizzeriaHall;
        public PizzeriaHallController(IGetPizzeriaHalls getPizzeriaHalls, IAddPizzeriaHall addPizzeriaHall, IGetPizzeriaHall getPizzeriaHall, IDeletePizzeriaHall deletePizzeriaHall, IUpdatePizzeriaHall updatePizzeriaHall)
        {
            this.getPizzeriaHalls = getPizzeriaHalls;
            this.addPizzeriaHall = addPizzeriaHall;
            this.getPizzeriaHall = getPizzeriaHall;
            this.deletePizzeriaHall = deletePizzeriaHall;
            this.updatePizzeriaHall = updatePizzeriaHall;
        }
        // GET: api/PizzeriaHall
       
        /// <summary>
        /// Gets PizzeriaHalls
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /PizzeriaHallSearch
        ///     {
        ///        "Name":"Ime"
        ///     }
        ///
        /// </remarks>
        /// <param name="value"></param>
        /// <returns>PizzeriaHalls</returns>
        /// <response code="201">PizzeriaHalls with the similar or same name</response>
        [HttpGet]
        public ActionResult<IEnumerable<PizzeriaHallDTO>> Get([FromQuery] PizzeriaHallSearch search)
        {
            try
            {
                return Ok(getPizzeriaHalls.Execute(search));
            }
            catch(Exception e)
            {
                return StatusCode(500);
            }
        }

        // GET: api/PizzeriaHall/5
        /// <summary>
        /// Gets a PizzeriaHall
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /PizzeriaHall/Id
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>PizzeriaHall with the same id</returns>
        /// <response code="201">Returns the PizzeriaHall</response>
        /// <response code="400">If the PizzeriaHall is null</response>
        [HttpGet("{id}", Name = "GetRS")]
        public ActionResult<PizzeriaHallDTO> Get(int id)
        {
            try
            {
                return Ok(getPizzeriaHall.Execute(id));
            }
            catch(NotFoundObjectException e)
            {
                return NotFound(e.Message);
            }
            catch(Exception e)
            {
                return StatusCode(500);
            }
        }
        
        /// <summary>
        /// Creates a PizzeriaHall.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post /PizzeriaHall
        ///     {
        ///        "Name": "Roletralala"
        ///     }
        ///
        /// </remarks>
        /// <param name="value"></param>
        /// <returns>A newly created PizzeriaHall</returns>
        /// <response code="204">Returns a PizzeriaHall</response>
        /// <response code="400">If the PizzeriaHall is null</response>
        // POST: api/PizzeriaHall
        [LoggedIn("Manager")]
        [HttpPost]
        public ActionResult<PizzeriaHallDTO> Post([FromBody] PizzeriaHallDTO value)
        {
            try
            {
                var obj = addPizzeriaHall.Execute(value);
                return Created("api/ItemType/" + obj.Id, obj);
            }
            catch (ObjectAlreadyExistsException e)
            {
                return StatusCode(404);
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }

        // PUT: api/PizzeriaHall/5
        // PUT: api/PizzeriaHall/5
        /// <summary>
        /// Updates a PizzeriaHall.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /PizzeriaHallRequest
        ///     {
        ///        "Name": "HallRandom"
        ///     }
        ///
        /// </remarks>
        /// <param name="value"></param>
        /// <returns>No Content</returns>
        /// <response code="204">Returns No Content</response>
        /// <response code="400">If the Name is already used</response>
        [LoggedIn("Manager")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PizzeriaHallDTO value)
        {
            try
            {
                this.updatePizzeriaHall.Execute(value, id);
                return NoContent();
            }
            catch(NotFoundObjectException e)
            {
                return NotFound(e.Message);
            }
            catch(ObjectAlreadyExistsException e)
            {
                return UnprocessableEntity(e.Message);
            }
            catch(Exception e)
            {
                return StatusCode(500);
            }
        }

        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// Deletes a specific Pizzeria Hall.
        /// </summary>
        /// <param name="id"></param>   
        [LoggedIn("Manager")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                this.deletePizzeriaHall.Execute(id);
                return NoContent();
            }
            catch(NotFoundObjectException e)
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
