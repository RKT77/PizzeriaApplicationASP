using DataAccess;
using PizzeriaApplication.DTO;
using PizzeriaApplication.Exceptions;
using PizzeriaApplication.ICommands.ICommandsAttendant;
using PizzeriaApplication.Interfaces;
using PizzeriaApplication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzeriaCommands.AttendantCommands
{
    public class UpdateAttendant : BaseCommand, IUpdateAttendant
    {
        public UpdateAttendant(PizzeriaContext ctx) : base(ctx)
        {
        }

        public void Execute(AttendantRequest req, int id)
        {
            var update = this.context.Attendants.Find(id);
            if (update != null)
            {
                if (req.FirstName!=null)
                    update.FirstName = req.FirstName;
                if (req.LastName != null)
                    update.LastName = req.LastName;
                if (req.Email != null)
                    update.Email = req.Email;
                if (req.IdRole != null)
                {
                    if (context.Roles.Any(p => p.Id == req.IdRole))
                    {
                        update.IdRole = req.IdRole;
                    }
                    else
                    {
                        throw new ObjectDoesntExistException("Role");
                    }
                }
                this.context.SaveChanges();
            }
            else
            {
                throw new NotFoundObjectException("Attendant");
            }
        }
    }
}
