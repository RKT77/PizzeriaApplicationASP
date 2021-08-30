using DataAccess;
using Domain;
using PizzeriaApplication.DTO;
using PizzeriaApplication.Exceptions;
using PizzeriaApplication.ICommands.ICommandsItemType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzeriaCommands.ItemTypeCommands
{
    public class AddItemType : BaseCommand, IAddItemType
    {
        IGetItemType getItemType;
        public AddItemType(PizzeriaContext context,IGetItemType getItemType) : base(context)
        {
            this.getItemType = getItemType;
        }
        public ItemTypeDTO Execute(ItemTypeDTO req)
        {
            if (context.ItemTypes.Any(p => p.Name == req.Name && p.IsDeleted==false))
            {
                throw new ObjectAlreadyExistsException("Item");
            }
            else
            {
                var obj = new ItemType
                {
                    Name = req.Name
                };
                this.context.ItemTypes.Add(obj);
                context.SaveChanges();
                return getItemType.Execute(obj.Id);
            }      
        }
       
    }
}
