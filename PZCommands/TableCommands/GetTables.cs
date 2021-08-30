using DataAccess;
using Microsoft.EntityFrameworkCore;
using PizzeriaApplication.DTO;
using PizzeriaApplication.ICommands.ICommandsTable;
using PizzeriaApplication.Responses;
using PizzeriaApplication.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzeriaCommands.TableCommands
{
    public class GetTables : BaseCommand, IGetTables
    {
        public GetTables(PizzeriaContext ctx) : base(ctx)
        {
        }

        public IEnumerable<TableDTO> Execute(TableSearch req)
        {
            var tables = this.context.Tables
                .AsQueryable();

            if (req.IdPizzeriaHall != null)
            {
                tables = tables.Where(p => p.IdPizzeriaHall == req.IdPizzeriaHall);
            }
            if (req.IsFree != null)
            {
                if (req.IsFree == true)
                {
                    tables = tables.Where(p => p.Orders.All(x => x.Active == false));
                }
                else
                {
                    tables = tables.Where(p => p.Orders.Any(x => x.Active == true));
                }
            }
       

            tables = tables
               .Include(p => p.PizzeriaHall)
               .Include(p => p.Orders)
               .Where(p => p.IsDeleted == false);


            return tables.Select(p => new TableDTO
            {
                Id = p.Id,
                Name = p.Name,
                Hall = p.PizzeriaHall.Name
            });
        }
    }
}
