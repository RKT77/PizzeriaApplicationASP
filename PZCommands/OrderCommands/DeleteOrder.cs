using DataAccess;
using PizzeriaApplication.Exceptions;
using PizzeriaApplication.ICommands.ICommandsOrder;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaCommands.OrderCommands
{
    public class DeleteOrder : BaseCommand, IDeleteOrder
    {
        public DeleteOrder(PizzeriaContext ctx) : base(ctx)
        {
        }

        public void Execute(int req)
        {
            var delete = this.context.Orders.Find(req);
            if (delete != null)
            {
                delete.IsDeleted = true;
                delete.Active = false;
                delete.IsPaid = false;
                delete.IsCancelled = false;
                this.context.SaveChanges();
            }
            else
            {
                throw new ObjectDoesntExistException("Order");
            }
        }
    }
}
