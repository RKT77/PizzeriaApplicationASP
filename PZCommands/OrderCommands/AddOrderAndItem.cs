using DataAccess;
using Domain;
using PizzeriaApplication.Exceptions;
using PizzeriaApplication.ICommands.ICommandsOrder;
using PizzeriaApplication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzeriaCommands.OrderCommands
{
    public class AddOrderAndItem : BaseCommand, IAddOrderAndItem
    {
        public AddOrderAndItem(PizzeriaContext ctx) : base(ctx)
        {
        }

        public void Execute(OrderRequest req)
        {
            var item = context.Items.Find(req.IdItem);
            var order = new Order();
            if (this.context.Tables.Any(p => p.Id == req.IdTable))
            {
                order.IdTable = (int)req.IdTable;
            }
            else
            {
                throw new NotFoundObjectException("Table");
            }

            if (this.context.Attendants.Any(p => p.Id == req.IdAttendant))
            {
                order.IdAttendant = (int)req.IdAttendant;
            }
            else
            {
                throw new NotFoundObjectException("Attendant");
            }
            var OrderItem = new OrderItem();
            if (this.context.Items.Any(p => p.Id == req.IdItem))
            {
                OrderItem.IdItem = req.IdItem;
            }
            else
            {
                throw new NotFoundObjectException("Item");
            }
            OrderItem.ItemPrice = item.Price;
            OrderItem.ItemsNumber = 1;
            OrderItem.Order = order;

            this.context.OrderItems.Add(OrderItem);
            this.context.SaveChanges();
        }
    }
}
