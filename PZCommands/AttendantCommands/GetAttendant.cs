using DataAccess;
using Microsoft.EntityFrameworkCore;
using PizzeriaApplication.DTO;
using PizzeriaApplication.Exceptions;
using PizzeriaApplication.ICommands.ICommandsAttendant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzeriaCommands.AttendantCommands
{
    public class GetAttendant : BaseCommand, IGetAttendant
    {
        public GetAttendant(PizzeriaContext ctx) : base(ctx)
        {
        }

        public AttendantDTO Execute(int req)
        {
            var Attendant = context.Attendants.AsQueryable().Include(p => p.Role).Where(x=>x.Id==req).FirstOrDefault();
            if (Attendant != null)
            {
                return new AttendantDTO
                {
                    Id = Attendant.Id,
                    FirstName = Attendant.FirstName,
                    LastName = Attendant.LastName,
                    Role = Attendant.Role.Name,
                    Email = Attendant.Email,
                    Password = Attendant.Password
                };
            }
            else
            {
                throw new NotFoundObjectException("Attendant");
            }
        }
    }
}
