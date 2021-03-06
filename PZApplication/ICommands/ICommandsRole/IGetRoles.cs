using PizzeriaApplication.DTO;
using PizzeriaApplication.Interfaces;
using PizzeriaApplication.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaApplication.ICommands.ICommandsRole
{
    public interface IGetRoles:ICommand<RoleSearch,IEnumerable<RoleDTO>>
    {
    }
}
