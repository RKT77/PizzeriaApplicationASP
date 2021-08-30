using Domain;
using PizzeriaApplication.Interfaces;
using PizzeriaApplication.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaApplication.ICommands.ICommandsOrder
{
    public interface IAddItemToOrder: IUpdateCommand<OrderRequest,Order>
    {
    }
}
