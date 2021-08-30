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
    public class ChangeTable : BaseCommand, IChangeTable
    {
        public ChangeTable(PizzeriaContext ctx) : base(ctx)
        {
        }

        public void Execute(int req, TableChangeRequest table)
        {
            
            var change = this.context.Orders.Find(req);
            if (change != null)
            {
                if (context.Tables.Any(p => p.Id == table.IdTable))
                {
                    change.IdTable = table.IdTable;
                    change.ModifiedAt = DateTime.Now;
                    this.context.SaveChanges();
                }
                else
                {
                    throw new ObjectDoesntExistException("Table");
                }
            }
            else
            {
                throw new ObjectDoesntExistException("Order");
            }
        }
    }
}
