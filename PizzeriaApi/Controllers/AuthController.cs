﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PizzeriaApplication.Exceptions;
using PizzeriaApplication.ICommands.ICommandsAuth;
using PizzeriaApplication.Login;
using PizzeriaApplication.Requests;
using PizzeriaApi.Helpers;

namespace PizzeriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly Encryption _enc;
        readonly IGetLoggedUser getLoggedUser;

        public AuthController(IGetLoggedUser getLoggedUser, Encryption enc)
        {
            _enc = enc;
            this.getLoggedUser = getLoggedUser;
        }

        [HttpPost]
        public IActionResult Post(LoginRequest login)
        {
               
            try
            {
                var user = getLoggedUser.Execute(login);
                var stringObjekat = JsonConvert.SerializeObject(user);

                var encrypted = _enc.EncryptString(stringObjekat);

                return Ok(new { token = encrypted });
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
