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
    public class ClientCardController : Controller
    {
        private AppDbContext _context;
        public ClientCardController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var myClientCards = _context.ClientCards.ToList();
            return View(myClientCards);
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Edit(int id)
        {
            return View(_context.ClientCards.Where(x => x.Id == id).FirstOrDefault());
        }
        public IActionResult EditClientCard(int id, int client_id, int order_id)
        {
            using (_context)
            {
                var clientCard = _context.ClientCards.Where(x => x.Id == id).FirstOrDefault();
                clientCard.ClientId = client_id;
                clientCard.OrderId = order_id;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
