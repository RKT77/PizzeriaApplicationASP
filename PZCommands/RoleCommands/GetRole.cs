using DataAccess;
using PizzeriaApplication.DTO;
using PizzeriaApplication.Exceptions;
using PizzeriaApplication.ICommands.ICommandsRole;
using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaCommands.RoleCommands
{
    public class GetRole : BaseCommand, IGetRole
    {
        public GetRole(PizzeriaContext ctx) : base(ctx)
        {
        }

        public RoleDTO Execute(int req)
        {
            var role = context.Roles.Find(req);
            if (role!=null)
            {
                return new RoleDTO
                {
                    Id=role.Id,
                    Name=role.Name
                };
            }
            else
            {
                throw new NotFoundObjectException("Role");
            }
        }
    }
}
