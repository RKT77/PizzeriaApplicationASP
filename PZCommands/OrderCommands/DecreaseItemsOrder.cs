using DataAccess;
using PizzeriaApplication.Exceptions;
using PizzeriaApplication.ICommands.ICommandsOrder;
using PizzeriaApplication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzeriaCommands.OrderCommands
{
    public class SubtractItemsOrder : BaseCommand, ISubtractItemsOrder
    {
        IDeleteOrder deleteOrder;
        public SubtractItemsOrder(PizzeriaContext ctx,IDeleteOrder deleteOrder) : base(ctx)
        {
            this.deleteOrder = deleteOrder;
        }

        public void Execute(ItemSubtractRequest req, int IdOrder)
        {
            var IdItem = req.IdItem;
            if (this.context.Orders.Find(IdOrder).Active == true)
            {
                var itemOrder = this.context.OrderItems.AsQueryable()
                    .Where(p => p.IdItem == IdItem)
                    .Where(p => p.IdOrder == IdOrder).FirstOrDefault();
                if (req.DeleteAll == 1)
                {
                    this.context.OrderItems.Remove(itemOrder);
                    this.context.SaveChanges();
                    if (!this.context.OrderItems.Any(p => p.IdOrder == itemOrder.IdOrder))
                    {
                        this.deleteOrder.Execute(itemOrder.IdOrder);
                    }
                }
                else
                {
                    if (req.DeleteAll == 0)
                    {
                        itemOrder.ItemsNumber = itemOrder.ItemsNumber - 1;
                        if (itemOrder.ItemsNumber == 0)
                        {
                            this.context.OrderItems.Remove(itemOrder);
                            this.context.SaveChanges();
                            if (!this.context.OrderItems.Any(p => p.IdOrder == itemOrder.IdOrder))
                            {
                                this.deleteOrder.Execute(itemOrder.IdOrder);
                            }
                        }
                    }
                    else
                    {
                        throw new ObjectDoesntExistException("Number");
                    }
                }
            }
            else
            {
                throw new ObjectDoesntExistException("Order");
            }
            this.context.SaveChanges();
        }
    }
}
