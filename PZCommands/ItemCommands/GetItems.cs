using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PizzeriaApplication.DTO;
using PizzeriaApplication.Exceptions;
using PizzeriaApplication.ICommands.ICommandsItem;
using PizzeriaApplication.Responses;
using PizzeriaApplication.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzeriaCommands.ItemCommands
{
    public class GetItems : BaseCommand, IGetItems
    {
        public GetItems(PizzeriaContext ctx) : base(ctx)
        {
        }

        public PagedResponse<ItemDTO> Execute(ItemSearch req)
        {
            var items = context.Items
                .AsQueryable();

            var id = req.IdItemType;
            if (req.Keyword != null)
            {
                items = items.Where(p => p.Name.ToLower().Contains(req.Keyword.ToLower()));
            }
            if (req.IdItemType != null)
            {
                if (context.ItemTypes.Any(p => p.Id == req.IdItemType))
                {
                    items = items.Where(p => p.IdItemType == req.IdItemType);
                }
                else
                {
                    throw new NotFoundObjectException("ItemType");
                }
            }
            if (req.IdOrder != null)
            {
                items = items.Where(p => p.OrderItems.Where(x => x.IdOrder == req.IdOrder).Any(y => y.IdItem == p.Id));
            }
            var totalCount = items.Count();

            items = items
               .Include(p => p.ItemType)
               .Include(p => p.OrderItems)
               .Where(p => p.IsDeleted == false).Skip((req.PageNumber - 1) * req.PerPage).Take(req.PerPage);

            var pagesCount = (int)Math.Ceiling((double)totalCount / req.PerPage);


            return new PagedResponse<ItemDTO>
            {
                CurrentPage = req.PageNumber,
                TotalCount = totalCount,
                PagesCount = pagesCount,
                Data = items.Select(p => new ItemDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    ItemType = p.ItemType.Name,
                    Image = p.Image
                })
            };
        }
    }
}
