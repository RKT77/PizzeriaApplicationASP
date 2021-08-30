using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzeriaApplication.DTO;
using PizzeriaApplication.Exceptions;
using PizzeriaApplication.ICommands.ICommandsItem;
using PizzeriaApplication.ICommands.ICommandsOrder;
using PizzeriaApplication.ICommands.ICommandsTable;
using PizzeriaApplication.ICommands.ICommandsAttendant;
using PizzeriaApplication.Requests;
using PizzeriaApplication.Searches;
using PizzeriaMVC.Models;

namespace PizzeriaMVC.Controllers
{
    public class OrdersController : Controller
    {
        readonly IGetOrders getOrders;
        readonly IGetOrder getOrder;
        readonly IGetItems getItems;
        readonly IGetAttendants getAttendants;
        readonly IGetTables getTables;
        readonly IAddOrderItem addOrderItem;
        readonly IChangeTable changeTable;
        readonly ISubtractItemsOrder decreaseItemsOrder;
        readonly IDeleteOrder deleteOrder;
        readonly IChangeStatus changeStatus;
        public OrdersController(IChangeStatus changeStatus, IDeleteOrder deleteOrder, ISubtractItemsOrder decreaseItemsOrder, IChangeTable changeTable, IAddOrderItem addOrderItem,IGetTables getTables, IGetItems getItems, IGetAttendants getAttendants, IGetOrders getOrders,IGetOrder getOrder)
        {
            this.getOrders = getOrders;
            this.getOrder = getOrder;
            this.getTables = getTables;
            this.getAttendants = getAttendants;
            this.getItems = getItems;
            this.addOrderItem = addOrderItem;
            this.changeTable = changeTable;
            this.decreaseItemsOrder = decreaseItemsOrder;
            this.deleteOrder = deleteOrder;
            this.changeStatus = changeStatus;
        }
        // GET: Orders
        public ActionResult Index([FromQuery] OrderSearch orderSearch)
        {
            try
            {
                return View(getOrders.Execute(orderSearch));
            }
            catch (ObjectDoesntExistException e)
            {
                TempData["error"] = e.Message;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["error"] = "Server error";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Orders/Details/5
        public ActionResult Details(int id)
        {
            try
            { 
                return View(getOrder.Execute(id));
            }
            catch (NotFoundObjectException e)
            {
                TempData["Error"] = e.Message;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                TempData["Error"] = "Server error";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            var co = new CreateOrderModel();
            co.Items = getItems.Execute(new ItemSearch() { PerPage = 1000 }).Data;
            co.Tables = getTables.Execute(new TableSearch() { IsFree=true });
            co.Attendants = getAttendants.Execute(new AttendantSearch());
            return View(co);
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateOrderModel model)
        {

            if (!ModelState.IsValid)
            {
                model.Items = getItems.Execute(new ItemSearch() { PerPage = 1000 }).Data;
                model.Tables = getTables.Execute(new TableSearch() { IsFree = true });
                model.Attendants = getAttendants.Execute(new AttendantSearch());
                return View(model);
            }
            try
            {
                this.addOrderItem.Execute(model.OrderRequest);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundObjectException e)
            {
                TempData["Error"] = e.Message;
                return View(nameof(Index));
            }
            catch (Exception e)
            {
                TempData["Error"] = "Server error";
                return View(nameof(Index));
            }
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int id)
        {
            var OrderUpdate = new OrderUpdate();
            OrderUpdate.orderDTO = getOrder.Execute(id);
            OrderUpdate.Items = getItems.Execute(new ItemSearch { IdOrder = id,PerPage=1000 }).Data;
            OrderUpdate.Tables = getTables.Execute(new TableSearch { IsFree = true });
            OrderUpdate.ItemsAll = getItems.Execute(new ItemSearch()).Data;
            return View(OrderUpdate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,OrderUpdate orderUpdate)
        {
            if (!ModelState.IsValid)
            {
                orderUpdate.orderDTO = getOrder.Execute(id);
                orderUpdate.Items = getItems.Execute(new ItemSearch { IdOrder = id, PerPage = 1000 }).Data;
                orderUpdate.Tables = getTables.Execute(new TableSearch { IsFree = true });
                orderUpdate.ItemsAll = getItems.Execute(new ItemSearch()).Data;
                return View(orderUpdate);
            }
            if (orderUpdate.TableChangeRequest != null)
            {
                try
                {
                    this.changeTable.Execute(id, orderUpdate.TableChangeRequest);
                    return RedirectToAction(nameof(Index));
                }
                catch (NotFoundObjectException e)
                {
                    TempData["Error"] = e.Message;
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    TempData["Error"] = "Server error";
                    return RedirectToAction(nameof(Index));
                }
            }
            if (orderUpdate.ItemSubtractRequest != null)
            {
                try
                {
                    this.decreaseItemsOrder.Execute(orderUpdate.ItemSubtractRequest, id);
                    return RedirectToAction(nameof(Index));
                }
                catch (ObjectDoesntExistException e)
                {
                    TempData["Error"] = e.Message;
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    TempData["Error"] = "Server error";
                    return RedirectToAction(nameof(Index));
                }
            }
            if (orderUpdate.OrderRequest != null)
            {
                try
                {
                    orderUpdate.OrderRequest.IdTable = getOrder.Execute(id).IdTable;
                    this.addOrderItem.Execute(orderUpdate.OrderRequest);
                    return RedirectToAction(nameof(Edit));
                }
                catch (NotFoundObjectException e)
                {
                    TempData["Error"] = e.Message;
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    TempData["Error"] = "Server error";
                    return RedirectToAction(nameof(Index));
                }
            }
            if (orderUpdate.Status != null)
            {
                try
                {
                    this.changeStatus.Execute(new StatusRequest { status =  orderUpdate.Status }, id);   
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    TempData["Error"] = "Server error";
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int id)
        {
            return View(getOrder.Execute(id));
        }

        // POST: Orders/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                deleteOrder.Execute(id);
                return RedirectToAction(nameof(Index));
            }
            catch (ObjectDoesntExistException e)
            {
                TempData["Error"] = e.Message;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                TempData["Error"] = "Server error";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
