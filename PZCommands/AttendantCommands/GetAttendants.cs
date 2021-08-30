using DataAccess;
using Microsoft.EntityFrameworkCore;
using PizzeriaApplication.DTO;
using PizzeriaApplication.Exceptions;
using PizzeriaApplication.ICommands.ICommandsAttendant;
using PizzeriaApplication.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzeriaCommands.AttendantCommands
{
    public class GetAttendants : BaseCommand, IGetAttendants
    {
        public GetAttendants(PizzeriaContext ctx) : base(ctx)
        {
        }

        public IEnumerable<AttendantDTO> Execute(AttendantSearch req)
        {
            var Attendant = this.context.Attendants.AsQueryable().Include(p => p.Role).Where(p=>p.IsDeleted==false);

            if (req.IdRole != null)
            {
                if (this.context.Roles.Any(p => p.Id == req.IdRole))
                {
                    return Attendant.Where(p => p.IdRole == req.IdRole).Select(p => new AttendantDTO
                    {
                        Id = p.Id,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        Role = p.Role.Name,
                        Email = p.Email,
                        Password = p.Password
                    });
                }
                else
                {
                    throw new ObjectDoesntExistException("Role");
                }
            }
            else
            {
                return Attendant.Select(p => new AttendantDTO
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Role = p.Role.Name,
                    Email = p.Email
                });
            }
        }
    }
}
