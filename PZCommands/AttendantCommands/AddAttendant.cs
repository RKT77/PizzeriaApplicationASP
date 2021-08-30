using DataAccess;
using Domain;
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
    public class AddAttendant : BaseCommand,IAddAttendant
    {
        private IGetAttendant getAttendant;
        private IEmailSender emailSender;
        public AddAttendant(IGetAttendant getAttendant,PizzeriaContext ctx,IEmailSender emailSender) : base(ctx)
        {
            this.getAttendant = getAttendant;
            this.emailSender = emailSender;
        }

        public AttendantDTO Execute(AttendantRequest req)
        {
            if (context.Roles.Any(p => p.Id == req.IdRole))
            {
                if (context.Attendants.Any(p => p.Email == req.Email))
                {
                    throw new ObjectAlreadyExistsException("Email");
                }
                else
                {
                    var Attendant = new Attendant
                    {
                        FirstName = req.FirstName,
                        LastName = req.LastName,
                        IdRole = req.IdRole,
                        Email = req.Email,
                        Password = req.Password
                    };
                    this.context.Attendants.Add(Attendant);
                    this.context.SaveChanges();
                    emailSender.Subject = "Hiring";
                    emailSender.Subject = req.FirstName + " " + req.LastName + ", you have been hired!";
                    emailSender.ToEmail = req.Email;
                    emailSender.Send();
                    return getAttendant.Execute(Attendant.Id);
                }
            }
            else
            {
                throw new ObjectDoesntExistException("Role");
            }
        }
    }
}
