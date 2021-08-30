using PizzeriaApplication.Interfaces;
using PizzeriaApplication.Login;
using PizzeriaApplication.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaApplication.ICommands.ICommandsAuth
{
    public interface IGetLoggedUser:ICommand<LoginRequest,LoggedUser>
    {
    }
}
