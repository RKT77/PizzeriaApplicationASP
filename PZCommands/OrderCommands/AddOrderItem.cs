using DataAccess;
using Domain;
using PizzeriaApplication.DTO;
using PizzeriaApplication.ICommands.ICommandsOrder;
using PizzeriaApplication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzeriaCommands.OrderCommands
{
    public class AddOrderItem : BaseCommand, IAddOrderItem
    {
        IAddItemToOrder addOrderToItem;
        IAddOrderAndItem addOrderAndItem;
        public AddOrderItem(PizzeriaContext ctx,IAddOrderAndItem addOrderAndItem, IAddItemToOrder addOrderToItem) : base(ctx)
        {
            this.addOrderToItem = addOrderToItem;
            this.addOrderAndItem = addOrderAndItem;
        }

        public void Execute(OrderRequest req)
        {
            var OrderToAdd = this.context.Orders.Where(p => p.IdTable == req.IdTable).Where(p => p.Active == true).FirstOrDefault();
            if (OrderToAdd == null)
            {
                this.addOrderAndItem.Execute(req);
            }
            else
            {
                this.addOrderToItem.Execute(req, OrderToAdd); 
            }
        }
    }
}
