using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzeriaApplication.DTO;
using PizzeriaApplication.Exceptions;
using PizzeriaApplication.ICommands.ICommandsAttendant;
using PizzeriaApplication.Requests;
using PizzeriaApplication.Searches;
using PizzeriaApi.Helpers;

namespace PizzeriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendantController : ControllerBase
    {
        IGetAttendant getAttendant;
        IGetAttendants getAttendants;
        IAddAttendant addAttendant;
        IUpdateAttendant updateAttendant;
        IDeleteAttendant deleteAttendant;
        public AttendantController(IGetAttendant getAttendant,IAddAttendant addAttendant, IGetAttendants getAttendants, IUpdateAttendant updateAttendant, IDeleteAttendant deleteAttendant)
        {
            this.getAttendant = getAttendant;
            this.getAttendants = getAttendants;
            this.addAttendant = addAttendant;
            this.updateAttendant = updateAttendant;
            this.deleteAttendant = deleteAttendant;
        }
        /// <summary>
        /// Gets Attendants
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /AttendantSearch
        ///     {
        ///        "IdRole":1
        ///     }
        ///
        /// </remarks>
        /// <param name="value"></param>
        /// <returns>Attendants with the same role</returns>
        /// <response code="201">Returns the Attendant which has same IdRole</response>
        /// <response code="400">If the IdRole is null</response>    
        // GET: api/Attendant
        [HttpGet]
        public ActionResult<IEnumerable<AttendantDTO>> Get([FromQuery] AttendantSearch search)
        {
            try
            {
                return Ok(getAttendants.Execute(search));
            }
            catch(ObjectDoesntExistException e)
            {
                return UnprocessableEntity(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        /// <summary>
        /// Gets a Attendant
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Attendant/Id
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Attendants with the id</returns>
        /// <response code="201">Returns the Attendant</response>
        /// <response code="400">If the Attendant is null</response>    
        // GET: api/Attendant/5
        [HttpGet("{id}", Name = "GetAttendant")]
        public ActionResult<AttendantDTO> Get(int id)
        {
            try
            {
                return Ok(getAttendant.Execute(id));
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

        // POST: api/Attendant
        /// <summary>
        /// Creates an Attendant.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /AttendantRequest
        ///     {
        ///        "FirstnName": "Ime",
        ///        "LastName": "Prezime",
        ///        "IdRole": 1,
        ///        "Email":"email123@gmail.com
        ///     }
        ///
        /// </remarks>
        /// <param name="value"></param>
        /// <returns>A newly created Attendant</returns>
        /// <response code="201">Returns the newly created Attendant</response>
        /// <response code="400">If the Attendant is null</response>    
        [LoggedIn("Manager")]
        [HttpPost]
        public ActionResult<AttendantDTO> Post([FromBody] AttendantRequest value)
        {
            try
            {
                var Attendant = this.addAttendant.Execute(value);
                return Created("api/Attendant/" + Attendant.Id, Attendant);
            }
            catch(ObjectDoesntExistException e)
            {
                return UnprocessableEntity(e.Message);
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
        // PUT: api/Attendant
        /// <summary>
        /// Creates an Attendant.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /AttendantRequest
        ///     {
        ///        "FirstnName": "Ime",
        ///        "LastName": "Prezime",
        ///        "IdRole": 1,
        ///        "Email":"email123@gmail.com
        ///     }
        ///
        /// </remarks>
        /// <param name="value"></param>
        /// <returns>A newly created No Content</returns>
        /// <response code="204">Returns No Content</response>
        /// <response code="400">If the Attendant is null</response>    
        // PUT: api/Attendant/5
        [LoggedIn]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] AttendantRequest value)
        {
            try
            {
                this.updateAttendant.Execute(value, id);
                return NoContent();
            }
            catch(NotFoundObjectException e)
            {
                return NotFound(e.Message);
            }
            catch(ObjectDoesntExistException e)
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
        /// Deletes a specific Attendant.
        /// </summary>
        /// <param name="id"></param>   
        [LoggedIn("Manager")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                this.deleteAttendant.Execute(id);
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
