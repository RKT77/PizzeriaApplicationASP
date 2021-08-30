using PizzeriaApplication.DTO;
using PizzeriaApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaApplication.ICommands.ICommandsItem
{
    public interface IGetItem:ICommand<int,ItemDTO>
    {
    }
}
