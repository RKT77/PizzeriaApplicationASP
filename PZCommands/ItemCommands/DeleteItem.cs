using DataAccess;
using PizzeriaApplication.Exceptions;
using PizzeriaApplication.ICommands.ICommandsItem;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaCommands.ItemCommands
{
    public class DeleteItem : BaseCommand, IDeleteItem
    {
        public DeleteItem(PizzeriaContext ctx) : base(ctx)
        {
        }

        public void Execute(int req)
        {
            var update = this.context.Items.Find(req);
            if (update != null)
            {
                update.IsDeleted = true;
                this.context.SaveChanges();
            }
            else
            {
                throw new ObjectDoesntExistException("Item");
            }
        }
    }
}
