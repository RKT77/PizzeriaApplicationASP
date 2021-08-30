using DataAccess;
using PizzeriaApplication.Exceptions;
using PizzeriaApplication.ICommands.ICommandsItem;
using PizzeriaApplication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzeriaCommands.ItemCommands
{
    public class UpdateItem : BaseCommand, IUpdateItem
    {
        public UpdateItem(PizzeriaContext ctx) : base(ctx)
        {
        }

        public void Execute(ItemRequest req, int id)
        {
            var update = this.context.Items.Find(id);
            if (update != null)
            {
                if (req.ItemTypeId != null)
                {
                    if (this.context.ItemTypes.Any(p => p.Id == req.ItemTypeId))
                    {
                        update.IdItemType = req.ItemTypeId;
                    }
                    else
                    {
                        throw new ObjectDoesntExistException("ItemType");
                    }
                }
                if(req.Name!=null)
                update.Name = req.Name;
                if(req.Price!=null)
                update.Price = req.Price;
                this.context.SaveChanges();
            }
            else
            {
                throw new ObjectDoesntExistException("Item");
            }
        }
    }
}
