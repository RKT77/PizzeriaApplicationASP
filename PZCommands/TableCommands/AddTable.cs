using DataAccess;
using Domain;
using PizzeriaApplication.DTO;
using PizzeriaApplication.Exceptions;
using PizzeriaApplication.ICommands.ICommandsTable;
using PizzeriaApplication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzeriaCommands.TableCommands
{
    public class AddTable : BaseCommand,IAddTable
    {
        private GetTable getTable;
        public AddTable(PizzeriaContext ctx) : base(ctx)
        {
            getTable = new GetTable(ctx);
        }
        public TableDTO Execute(TableRequest req)
        {
            if (context.PizzeriaHalls.Any(p => p.Id == req.IdHall))
            {
                if (context.Tables.Any(p => p.Name == req.Name && p.IsDeleted==false))
                {
                    throw new ObjectAlreadyExistsException("Table");
                }
                else
                {
                    var obj = new Table
                    {
                        Name = req.Name,
                        IdPizzeriaHall = req.IdHall
                    };
                    context.Tables.Add(obj);
                    context.SaveChanges();
                    TableDTO dto = getTable.Execute(obj.Id);
                    return dto;
                }
            }
            else
            {
                throw new ObjectDoesntExistException("Pizzeria Hall");
            }
        }
    }
}
