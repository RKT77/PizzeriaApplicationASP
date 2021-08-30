using DataAccess;
using PizzeriaApplication.DTO;
using PizzeriaApplication.Exceptions;
using PizzeriaApplication.ICommands.ICommandsItemType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzeriaCommands.ItemTypeCommands
{
    public class UpdateItemType : BaseCommand,IUpdateItemType
    {
        public UpdateItemType(PizzeriaContext context) : base(context)
        {

        }
        public void Execute(ItemTypeDTO req,int id)
        {
                     
                if (this.context.ItemTypes.Any(p=>p.Id==id))
                {
                    if (this.context.ItemTypes.Any(p => p.Name == req.Name))
                    {
                        throw new ObjectAlreadyExistsException("ItemType");
                    }
                    else {
                        var update = this.context.ItemTypes.Find(id);
                        update.Name = req.Name;
                        context.SaveChanges();
                    }
                }
                else
                {
                    throw new ObjectDoesntExistException("ItemType");
                }
           
        }
    }
}
