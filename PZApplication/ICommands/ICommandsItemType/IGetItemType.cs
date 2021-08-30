using PizzeriaApplication.DTO;
using PizzeriaApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaApplication.ICommands.ICommandsItemType
{
    public interface IGetItemType:ICommand<int,ItemTypeDTO>
    {
    }
}
