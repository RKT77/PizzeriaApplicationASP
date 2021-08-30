using DataAccess;
using Domain;
using PizzeriaApplication.DTO;
using PizzeriaApplication.Exceptions;
using PizzeriaApplication.ICommands.ICommandsItem;
using PizzeriaApplication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzeriaCommands.ItemCommands
{
    public class AddItem : BaseCommand, IAddItem
    {
        public IGetItem getItem;

        public AddItem(PizzeriaContext context, IGetItem getItem) : base(context)
        {
            this.getItem = getItem;
        }

        public ItemDTO Execute(ItemRequest req)
        {
            if (this.context.ItemTypes.Any(p=>p.Id==req.ItemTypeId))
            {
                if (context.Items.Any(p => p.Name == req.Name && p.IsDeleted==false))
                {
                    throw new ObjectAlreadyExistsException("Item");
                }
                else
                {
                    
                    var item = new Item
                    {
                        Name = req.Name,
                        IdItemType = req.ItemTypeId,
                        Price = req.Price,
                        Image = req.Image
                    };
                    this.context.Items.Add(item);
                    this.context.SaveChanges();
                    return this.getItem.Execute(item.Id);
                }
            }
            else
            {
                throw new ObjectDoesntExistException("ItemType");
            }
        }
    }
}
