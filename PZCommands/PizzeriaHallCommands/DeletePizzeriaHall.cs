using DataAccess;
using PizzeriaApplication.Exceptions;
using PizzeriaApplication.ICommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaCommands
{
    public class DeletePizzeriaHall : BaseCommand, IDeletePizzeriaHall
    {
        public DeletePizzeriaHall(PizzeriaContext context) : base(context)
        {

        }
        public void Execute(int req)
        {
            var del = context.PizzeriaHalls.Find(req);
            if (del != null)
            {
                del.IsDeleted = true;
                this.context.PizzeriaHalls.Update(del);
                context.SaveChanges();
            }
            else
            {
                throw new NotFoundObjectException("Pizzeria Hall");
            }
        }
    }
}
