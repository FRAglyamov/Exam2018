using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Module1.Contexts;
using Module1.Models;

namespace Module1.Controllers
{
    public class InventoryController : Controller
    {
        private AppDbContext _context;
        public InventoryController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var myInventory = _context.Inventory.ToList();
            return View(myInventory);
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Edit(int id)
        {
            return View(_context.Inventory.Where(x => x.Id == id).FirstOrDefault());
        }
        public IActionResult EditInventory(int id, bool isAvailable)
        {
            using (_context)
            {
                var inventory = _context.Inventory.Where(x => x.Id == id).FirstOrDefault();
                inventory.IsAvaliable = isAvailable;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult AddInventory(string inventory_name)
        {
            using (_context)
            {
                var inventory = new Inventory
                {
                    Name = inventory_name
                };
                _context.Inventory.Add(inventory);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
