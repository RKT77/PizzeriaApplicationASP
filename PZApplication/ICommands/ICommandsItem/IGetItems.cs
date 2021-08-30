using PizzeriaApplication.DTO;
using PizzeriaApplication.Interfaces;
using PizzeriaApplication.Responses;
using PizzeriaApplication.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaApplication.ICommands.ICommandsItem
{
    public interface IGetItems:ICommand<ItemSearch,PagedResponse<ItemDTO>>
    {
    }
}
