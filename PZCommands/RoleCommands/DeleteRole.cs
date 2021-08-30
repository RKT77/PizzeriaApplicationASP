using DataAccess;
using PizzeriaApplication.Exceptions;
using PizzeriaApplication.ICommands.ICommandsRole;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaCommands.RoleCommands
{
    public class DeleteRole : BaseCommand, IDeleteRole
    {
        public DeleteRole(PizzeriaContext ctx) : base(ctx)
        {
        }

        public void Execute(int req)
        {
            var delete = this.context.Roles.Find(req);
            if (delete != null)
            {
                delete.IsDeleted = true;
                this.context.SaveChanges();
            }
            else
            {
                throw new ObjectDoesntExistException("Role");
            }
        }
    }
}
