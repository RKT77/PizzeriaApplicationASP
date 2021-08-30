using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzeriaApplication.Exceptions;
using PizzeriaApplication.ICommands.ICommandsItem;
using PizzeriaApplication.ICommands.ICommandsItemType;
using PizzeriaApplication.Requests;
using PizzeriaApplication.Searches;
using PizzeriaMVC.Models;

namespace PizzeriaMVC.Controllers
{
    public class ItemsController : Controller
    {
        readonly IGetItems getItems;
        readonly IGetItem getItem;
        readonly IGetItemTypes getItemTypes;
        readonly IAddItem addItem;
        readonly IUpdateItem updateItem;
        readonly IDeleteItem deleteItem;
        public ItemsController(IDeleteItem deleteItem, IAddItem addItem,IGetItems getItems,IGetItem getItem,IGetItemTypes getItemTypes, IUpdateItem updateItem)
        {
            this.getItems = getItems;
            this.getItem = getItem;
            this.getItemTypes = getItemTypes;
            this.addItem = addItem;
            this.updateItem = updateItem;
            this.deleteItem = deleteItem;
        }
        // GET: Items
        public ActionResult Index([FromQuery]ItemSearch itemSearch)
        {
            try
            {
                return View(getItems.Execute(itemSearch).Data);
            }
            catch (NotFoundObjectException e)
            {
                TempData["error"] = e.Message;
                return View();
            }
            catch (Exception e)
            {
                TempData["error"] = "Error on Server";
                return StatusCode(500);
            }
        }

        // GET: Items/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                return View(getItem.Execute(id));
            }
            catch (NotFoundObjectException e)
            {
                TempData["error"] = e.Message;
                return View();
            }
            catch (Exception e)
            {
                TempData["error"] = "Error on Server";
                return View();
            }
        }

        // GET: Items/Create
        public ActionResult Create()
        {
                var vm = new CreateItemModel();
                vm.ItemTypes = getItemTypes.Execute(new ItemTypeSearch());
                return View(vm);
        }

        // POST: Items/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateItemModel dto)
        {
            dto.ItemTypes = getItemTypes.Execute(new ItemTypeSearch());
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            try
            {
                this.addItem.Execute(dto.Item);
                return RedirectToAction(nameof(Index)); 
            }
            catch (ObjectDoesntExistException e)
            {
                TempData["error"]=e.Message;
                return View();
            }
            catch (ObjectAlreadyExistsException e)
            {
                TempData["error"] = e.Message;
                return View();
            }
            catch (Exception)
            {
                TempData["error"] = "Error on server";
                return View();
            }
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int id)
        {
            var vm = new CreateItemModel();
            var itemDTO = getItem.Execute(id);
            var item = new ItemRequest();
            item.Name = itemDTO.Name;
            item.Price = itemDTO.Price;
            vm.Item = item;
            vm.ItemTypes = getItemTypes.Execute(new ItemTypeSearch());
            return View(vm);
        }

        // POST: Items/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CreateItemModel dto)
        {
            if (!ModelState.IsValid)
            {
                dto.ItemTypes = getItemTypes.Execute(new ItemTypeSearch());
                return View(dto);
            }
            try
            {
                this.updateItem.Execute(dto.Item, id);
                return RedirectToAction(nameof(Index));
            }
            catch (ObjectDoesntExistException e)
            {
                TempData["error"] = e.Message;
                return View();
            }
            catch (ObjectAlreadyExistsException e)
            {
                TempData["error"] = e.Message;
                return View();
            }
            catch (Exception)
            {
                TempData["error"] = "Server error";
                return View();
            }
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int id)
        {
            return View(getItem.Execute(id));
        }

        // POST: Items/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {

                this.deleteItem.Execute(id);
                return RedirectToAction(nameof(Index));
            }
            catch (ObjectDoesntExistException e)
            {
                TempData["error"] = e.Message;
                return View();
            }
            catch (Exception)
            {
                TempData["error"] = "Server error";
                return View();
            }
        }
    }
}