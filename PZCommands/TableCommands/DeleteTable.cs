using DataAccess;
using PizzeriaApplication.Exceptions;
using PizzeriaApplication.ICommands.ICommandsTable;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaCommands.TableCommands
{
    public class DeleteTable : BaseCommand, IDeleteTable
    {
        public DeleteTable(PizzeriaContext ctx) : base(ctx)
        {
           
        }

        public void Execute(int req)
        {
            var delete = this.context.Tables.Find(req);
            if (delete != null)
            {
                delete.IsDeleted = true;
                this.context.SaveChanges();
            }
            else
            {
                throw new ObjectDoesntExistException("Table");
            }
        }
    }
}
