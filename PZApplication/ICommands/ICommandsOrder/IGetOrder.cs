using PizzeriaApplication.DTO;
using PizzeriaApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaApplication.ICommands.ICommandsOrder
{
    public interface IGetOrder:ICommand<int,OrderDTO>
    {
    }
}
