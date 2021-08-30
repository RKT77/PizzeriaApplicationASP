using DataAccess;
using Microsoft.EntityFrameworkCore;
using PizzeriaApplication.DTO;
using PizzeriaApplication.Exceptions;
using PizzeriaApplication.ICommands.ICommandsOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzeriaCommands.OrderCommands
{
    public class GetOrder : BaseCommand, IGetOrder
    {
        public GetOrder(PizzeriaContext ctx) : base(ctx)
        {
        }

        public OrderDTO Execute(int req)
        {
            
            var p = this.context.Orders.AsQueryable().
                Include(x => x.Attendant).
                Include(x => x.Table).
                Include(x => x.OrderItems)
                .ThenInclude(x => x.Item)
                .Where(x => x.Id == req)
                .Where(x=>x.IsDeleted==false)
                .FirstOrDefault();
            if (p != null)
            {
                return new OrderDTO
                {
                    Id = p.Id,
                    FullName = p.Attendant.FirstName + " " + p.Attendant.LastName,
                    Table = p.Table.Name,
                    IdTable = p.Table.Id,
                    TotalPrice = p.OrderItems.Sum(x => x.ItemPrice * x.ItemsNumber),
                    Items = p.OrderItems.Select(x => new OrderItemsDTO
                    {
                        ItemName = x.Item.Name,
                        ItemNumber = x.ItemsNumber,
                        TotalItemsPrice = x.ItemsNumber * x.ItemPrice
                    }).ToList()
                };
            }
            else
            {
                throw new NotFoundObjectException("Order");
            }
        }
    }
}
