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
    public class GetPizzeriaHall : BaseCommand, IGetPizzeriaHall
    {
        public GetPizzeriaHall(PizzeriaContext context) : base(context)
        {

        }
        public PizzeriaHallDTO Execute(int req)
        {
            if(this.context.PizzeriaHalls.Find(req)!=null)
            {
                var hall = context.PizzeriaHalls.Find(req);
                var ResDto = new PizzeriaHallDTO
                {
                    Id = hall.Id,
                    Name = hall.Name
                };
                return ResDto;
            }
            else
            {
                throw new NotFoundObjectException("Pizzeria Hall");
            }
        }
    }
}
