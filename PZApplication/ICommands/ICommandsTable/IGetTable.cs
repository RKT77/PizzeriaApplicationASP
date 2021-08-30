using PizzeriaApplication.DTO;
using PizzeriaApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaApplication.ICommands.ICommandsTable
{
    public interface IGetTable:ICommand<int,TableDTO>
    {
    }
}
