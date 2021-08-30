using DataAccess;
using PizzeriaApplication.DTO;
using PizzeriaApplication.Exceptions;
using PizzeriaApplication.ICommands.ICommandsRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzeriaCommands.RoleCommands
{
    public class UpdateRole : BaseCommand, IUpdateRole
    {
        public UpdateRole(PizzeriaContext ctx) : base(ctx)
        {
        }

        public void Execute(RoleDTO req, int i)
        {
            var update = this.context.Roles.Find(i);
            if (update == null)
            {
                throw new ObjectDoesntExistException("Role");
            }
            else
            {
                if (context.Roles.Any(p => p.Name.ToLower()==req.Name.ToLower()))
                {
                    update.Name = req.Name;
                    update.ModifiedAt = DateTime.Now;
                    this.context.SaveChanges();
                }
                else
                {
                    throw new ObjectAlreadyExistsException("Role");
                }
            }
        }
    }
}
