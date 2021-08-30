﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PizzeriaApplication.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace PizzeriaApi.Helpers
{
    public class LoggedIn : Attribute, IResourceFilter
    {
        private readonly string _role;
        public LoggedIn(string role)
        {
            _role = role;
        }

        public LoggedIn()
        {

        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {

        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var user = context.HttpContext.RequestServices.GetService<LoggedUser>();

            if (!user.IsLogged)
            {
                context.Result = new UnauthorizedResult();
            }
            else
            {
                if (_role != null)
                {
                    if (user.Role != _role)
                    {
                        context.Result = new UnauthorizedResult();
                    }
                }
            }
        }
    }
}
