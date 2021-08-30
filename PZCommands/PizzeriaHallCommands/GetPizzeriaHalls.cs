using DataAccess;
using PizzeriaApplication.DTO;
using PizzeriaApplication.ICommands;
using PizzeriaApplication.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzeriaCommands
{
    public class GetPizzeriaHalls : BaseCommand, IGetPizzeriaHalls
    {
        public GetPizzeriaHalls(PizzeriaContext context) : base(context)
        {

        }

        public IEnumerable<PizzeriaHallDTO> Execute(PizzeriaHallSearch req)
        {
            var halls = context.PizzeriaHalls.AsQueryable();
            halls = halls.Where(p => p.IsDeleted == false);
            if (req.Name == null)
            {
                return halls.Select(rs=>new PizzeriaHallDTO
                {
                    Id=rs.Id,
                    Name=rs.Name
                });   
            }
            else
            {
                return halls.Where(rs => rs.Name.ToLower().Contains(req.Name.ToLower()))
                    .Select(rs => new PizzeriaHallDTO
                    {
                        Id = rs.Id,
                        Name = rs.Name
                    });
            }
        }
    }
}
