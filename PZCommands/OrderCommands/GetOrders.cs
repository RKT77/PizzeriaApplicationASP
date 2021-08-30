using DataAccess;
using Microsoft.EntityFrameworkCore;
using PizzeriaApplication.DTO;
using PizzeriaApplication.Exceptions;
using PizzeriaApplication.ICommands.ICommandsOrder;
using PizzeriaApplication.Searches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PizzeriaCommands.OrderCommands
{
    public class GetOrders : BaseCommand, IGetOrders
    {
        public GetOrders(PizzeriaContext ctx) : base(ctx)
        {
        }

        public IEnumerable<OrderDTO> Execute(OrderSearch req)
        {
            var orders = this.context.Orders.AsQueryable().
                Include(p => p.Attendant).
                Include(p => p.Table).
                Include(p => p.OrderItems)
                .ThenInclude(p => p.Item)
                .Where(p=>p.IsDeleted==false);

            if (req.Status != null)
            {
                var status = req.Status;
                if (status == Status.active)
                {
                    orders = orders.Where(p=>p.Active == true);
                }
                if (status == Status.paid)
                {
                    orders = orders.Where(p => p.IsPaid == true);
                }
                if (status == Status.cancelled)
                {
                    orders = orders.Where(p => p.IsCancelled == true);
                }
            }

            if (req.DateFrom!=null)
            {
                if (req.DateFrom > DateTime.Now)
                {
                    throw new ObjectDoesntExistException("Date");
                }
                orders = orders.Where(p => p.CreatedAt > req.DateFrom);
            }
            if (req.DateTo != null)
            {
                orders = orders.Where(p => p.CreatedAt < req.DateTo);
            }
            if (req.IdTable!=null)
            {
                if (context.Tables.Any(p => p.Id == req.IdTable))
                {
                    orders = orders.Where(p => p.IdTable == req.IdTable);
                }
                else
                {
                    throw new ObjectDoesntExistException("Table");
                }
            }
            if (req.IdAttendant != null)
            {
                if (context.Attendants.Any(p => p.Id == req.IdAttendant))
                {
                    orders = orders.Where(p => p.IdAttendant == req.IdAttendant);
                }
                else
                {
                    throw new ObjectDoesntExistException("Attendant");
                }
            }
            

            return orders.Select(p => new OrderDTO
            {
                Id=p.Id,
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
            });
        }
    }
}
