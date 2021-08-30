using DataAccess;
using Domain;
using PizzeriaApplication.DTO;
using PizzeriaApplication.Exceptions;
using PizzeriaApplication.ICommands.ICommandsRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzeriaCommands.RoleCommands
{
    public class AddRole : BaseCommand, IAddRole
    {
        IGetRole getRole;
        public AddRole(PizzeriaContext ctx,IGetRole getRole) : base(ctx)
        {
            this.getRole = getRole;
        }

        public RoleDTO Execute(RoleDTO req)
        {
            if (context.Roles.Any(p => p.Name.ToLower() == req.Name))
            {
                throw new ObjectAlreadyExistsException("Role");
            }
            else
            {
                var obj = new Role
                {
                    Name = req.Name
                };
                this.context.Roles.Add(obj);
                this.context.SaveChanges();
                return this.getRole.Execute(obj.Id);
            }
        }
    }
}
