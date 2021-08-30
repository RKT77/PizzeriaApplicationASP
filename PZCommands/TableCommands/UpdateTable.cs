using DataAccess;
using PizzeriaApplication.Exceptions;
using PizzeriaApplication.ICommands.ICommandsTable;
using PizzeriaApplication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzeriaCommands.TableCommands
{
    public class UpdateTable : BaseCommand, IUpdateTable
    {
        public UpdateTable(PizzeriaContext ctx) : base(ctx)
        {
        }

        public void Execute(TableRequest req, int tint)
        {
            if (this.context.Tables.Any(p => p.Name == req.Name))
            {
                throw new ObjectAlreadyExistsException("Table");
            }
            else
            {
                var update = this.context.Tables.Find(tint);
                if (update != null)
                {
                    if (context.PizzeriaHalls.Any(p => p.Id == req.IdHall))
                    {
                        if (req.Name != null)
                        {
                            update.Name = req.Name;
                        }
                        this.context.SaveChanges();
                    }
                    else
                    {
                        throw new ObjectDoesntExistException("Pizzeria Hall");
                    }
                }
                else
                {
                    throw new ObjectDoesntExistException("Table");
                }
            }
        }
    }
}
