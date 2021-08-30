using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using PizzeriaApplication.DTO;
using PizzeriaApplication.Exceptions;
using PizzeriaApplication.ICommands.ICommandsTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzeriaCommands.TableCommands
{
    public class GetTable : BaseCommand, IGetTable
    {
        public GetTable(PizzeriaContext ctx) : base(ctx)
        {
        }

        public TableDTO Execute(int req)
        {
            var table = context.Tables.AsQueryable().
                Include(t=>t.PizzeriaHall).
                Where(p=>p.Id==req).FirstOrDefault();
        
            
            if (table != null)
            {
                return new TableDTO
                {
                    Id = table.Id,
                    Name = table.Name,
                    Hall = table.PizzeriaHall.Name
                };
            }
            else
            {
                throw new NotFoundObjectException("Table");
            }
        }
    }
}
