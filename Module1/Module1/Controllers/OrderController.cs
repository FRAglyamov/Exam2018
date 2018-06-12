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
    public class OrderController : Controller
    {
        private AppDbContext _context;
        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var myOrder = _context.Orders.ToList();
            return View(myOrder);
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Edit(int id)
        {
            return View(_context.Orders.Where(x => x.Id == id).FirstOrDefault());
        }
        public IActionResult EditOrders(int id)
        {
            using (_context)
            {
                var order = _context.Orders.Where(x => x.Id == id).FirstOrDefault();
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult AddOrder(int clientcard_id, int inventory_id, float discount, DateTime time_start, float time, float total_cost)
        {
            using (_context)
            {
                var order = new Order
                {
                    
                };
                _context.Orders.Add(order);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
