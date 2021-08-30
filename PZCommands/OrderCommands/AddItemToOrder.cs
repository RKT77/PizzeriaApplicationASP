using DataAccess;
using Domain;
using PizzeriaApplication.ICommands.ICommandsOrder;
using PizzeriaApplication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzeriaCommands.OrderCommands
{
    public class AddItemToOrder : BaseCommand, IAddItemToOrder
    {
        public AddItemToOrder(PizzeriaContext ctx) : base(ctx)
        {
        }

        public void Execute(OrderRequest req, Order order)
        {
            var item = context.Items.Find(req.IdItem);

            var id = order.Id;
          
            var OrderItem = this.context.OrderItems
                .Where(p => p.IdItem == req.IdItem)
                .Where(p => p.IdOrder == id)
                .FirstOrDefault();
            if (OrderItem != null)
            {
                OrderItem.ItemsNumber = OrderItem.ItemsNumber + 1;
            }
            else
            {
                context.OrderItems.Add(new OrderItem
                {
                    IdItem = req.IdItem,
                    IdOrder = id,
                    ItemPrice = item.Price,
                    ItemsNumber = 1
                });
            }
            this.context.SaveChanges();
        }
    }
}
