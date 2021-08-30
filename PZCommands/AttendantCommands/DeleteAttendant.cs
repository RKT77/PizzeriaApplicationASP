using DataAccess;
using PizzeriaApplication.Exceptions;
using PizzeriaApplication.ICommands.ICommandsAttendant;
using PizzeriaApplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaCommands.AttendantCommands
{
    public class DeleteAttendant : BaseCommand, IDeleteAttendant
    {
        IEmailSender emailSender;
        public DeleteAttendant(PizzeriaContext ctx,IEmailSender emailSender) : base(ctx)
        {
            this.emailSender = emailSender;
        }

        public void Execute(int req)
        {
            var delete = this.context.Attendants.Find(req);
            if (delete != null)
            {
                delete.IsDeleted = true;
                this.context.SaveChanges();
            }
            else
            {
                throw new ObjectDoesntExistException("Attendant");
            }
            emailSender.Subject = "Fired";
            emailSender.Body = delete.FirstName + " " + delete.LastName + ", you do not work here anymore!";
            emailSender.ToEmail = delete.Email;
            emailSender.Send();
        }
    }
}
