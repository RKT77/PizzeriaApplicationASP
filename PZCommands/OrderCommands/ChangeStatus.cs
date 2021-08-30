using DataAccess;
using Microsoft.EntityFrameworkCore;
using PizzeriaApplication.DTO;
using PizzeriaApplication.ICommands.ICommandsOrder;
using PizzeriaApplication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzeriaCommands.OrderCommands
{
    public class ChangeStatus : BaseCommand, IChangeStatus
    {
        public ChangeStatus(PizzeriaContext ctx) : base(ctx)
        {
        }

        public void Execute(StatusRequest req, int i)
        {
            var change = this.context.Orders.AsQueryable().Where(p => p.Id == i).FirstOrDefault();
            if (req.status == Status.paid)
            {
                change.Active = false;
                change.IsPaid = true;
                change.IsCancelled = false;
                change.ModifiedAt = DateTime.Now;
            }
            if (req.status == Status.cancelled)
            {
                change.Active = false;
                change.IsCancelled = true;
                change.IsPaid = false;

                change.ModifiedAt = DateTime.Now;
            }
            if (req.status == Status.active)
            {
                change.Active = true;
                change.IsCancelled = false;
                change.IsPaid = false;
                change.IsDeleted = false;

                change.ModifiedAt = DateTime.Now;
            }
            this.context.SaveChanges();
        }
    }
}
