using DataAccess;
using Domain;
using PizzeriaApplication.DTO;
using PizzeriaApplication.Exceptions;
using PizzeriaApplication.ICommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzeriaCommands
{
    public class AddPizzeriaHall : BaseCommand, IAddPizzeriaHall
    {
        IGetPizzeriaHall getPizzeriaHall;
        public AddPizzeriaHall(PizzeriaContext context,IGetPizzeriaHall getPizzeriaHall):base(context)
        {
            this.getPizzeriaHall = getPizzeriaHall;
        }
        public PizzeriaHallDTO Execute(PizzeriaHallDTO req)
        {
            if (context.PizzeriaHalls.Any(p => p.Name == req.Name && p.IsDeleted==false))
            {
                throw new ObjectAlreadyExistsException("Pizzeria Hall");
            }
            else
            {
                var ResSec = new PizzeriaHall
                {
                    Name = req.Name
                };
                this.context.PizzeriaHalls.Add(ResSec);
                this.context.SaveChanges();
                return getPizzeriaHall.Execute(ResSec.Id);
            }
        }
    }
}
