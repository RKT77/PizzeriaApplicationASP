using PizzeriaApplication.DTO;
using PizzeriaApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaApplication.ICommands.ICommandsRole
{
    public interface IGetRole:ICommand<int,RoleDTO>
    {
    }
}
