using DataAccess;
using PizzeriaApplication.DTO;
using PizzeriaApplication.Exceptions;
using PizzeriaApplication.ICommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzeriaCommands
{
    public class UpdatePizzeriaHall : BaseCommand, IUpdatePizzeriaHall
    {
        public UpdatePizzeriaHall(PizzeriaContext context) : base(context)
        {

        }
        public void Execute(PizzeriaHallDTO req, int i)
        {
            var update = context.PizzeriaHalls.Find(i);
            if (update == null)
            {
                throw new NotFoundObjectException("Pizzeria Hall");

            }
            else
            {
                if (context.PizzeriaHalls.Any(p => p.Name == req.Name))
                {
                    throw new ObjectAlreadyExistsException("Pizzeria Hall");
                }
                else
                {
                    update.Name = req.Name;
                    update.ModifiedAt = DateTime.Now;
                    this.context.PizzeriaHalls.Update(update);
                    context.SaveChanges();
                }
            }
        }
    }
}
