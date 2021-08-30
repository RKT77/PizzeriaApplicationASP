using PizzeriaApplication.Interfaces;
using PizzeriaApplication.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaApplication.ICommands.ICommandsItem
{
    public interface IUpdateItem:IUpdateCommand<ItemRequest,int>
    {
    }
}
