using DataAccess;
using Microsoft.EntityFrameworkCore;
using PizzeriaApplication.DTO;
using PizzeriaApplication.Exceptions;
using PizzeriaApplication.ICommands.ICommandsItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzeriaCommands.ItemCommands
{
    public class GetItem : BaseCommand, IGetItem
    {
        public GetItem(PizzeriaContext ctx) : base(ctx)
        {
        }

        public ItemDTO Execute(int req)
        {
            var item = this.context.Items.AsQueryable().Include(p => p.ItemType).Where(p=>p.IsDeleted==false).Where(p=>p.Id==req).FirstOrDefault();
            if (item != null)
            {
                return new ItemDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    ItemType = item.ItemType.Name,
                    Price = item.Price,
                    Image=item.Image
                };
            }
            else
            {
                throw new NotFoundObjectException("Item");
            }
        }
    }
}
