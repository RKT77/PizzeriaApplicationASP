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
    public class GetItemType : BaseCommand, IGetItemType
    {
        public GetItemType(PizzeriaContext context) : base(context)
        {

        }

        public ItemTypeDTO Execute(int req)
        {
            
            var item = context.ItemTypes.AsQueryable().Where(p=>p.Id==req).Where(p=>p.IsDeleted==false).FirstOrDefault();
            if (item != null)
            {
                return new ItemTypeDTO
                {
                    Id = item.Id,
                    Name = item.Name
                };
            }
            else
            {
                throw new NotFoundObjectException("ItemType");
            }
                
        }
    }
}
