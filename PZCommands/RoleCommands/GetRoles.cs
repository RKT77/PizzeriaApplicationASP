using DataAccess;
using PizzeriaApplication.DTO;
using PizzeriaApplication.ICommands.ICommandsRole;
using PizzeriaApplication.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzeriaCommands.RoleCommands
{
    public class GetRoles : BaseCommand, IGetRoles
    {
        public GetRoles(PizzeriaContext ctx) : base(ctx)
        {

        }

        public IEnumerable<RoleDTO> Execute(RoleSearch req)
        {
            var roles= this.context.Roles.AsQueryable().Where(p=>p.IsDeleted==false);
            if (req.Name != null)
            {
                roles = roles.Where(p => p.Name.ToLower().Contains(req.Name.ToLower()));
            }
            
                return roles.Select(p=>new RoleDTO
                {
                    Id=p.Id,
                    Name=p.Name
                });
            
        }
    }
}
