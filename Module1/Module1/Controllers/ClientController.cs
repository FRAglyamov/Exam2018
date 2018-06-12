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
    public class ClientController : Controller
    {
        private AppDbContext _context;
        public ClientController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var myClients = _context.Clients.ToList();
            return View(myClients);
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Edit(int id)
        {
            return View(_context.Clients.Where(x => x.Id == id).FirstOrDefault());
        }
        public IActionResult EditClient(int id, string client_name, string client_surname, int client_serialnumber, DateTime client_date, string client_registration)
        {
            using (_context)
            {
                var client = _context.Clients.Where(x => x.Id == id).FirstOrDefault();
                client.Name = client_name;
                client.Surname = client_surname;
                client.SerialNumber = client_serialnumber;
                client.Birthdate = client_date;
                client.Registration = client_registration;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult AddClient(string client_name, string client_surname, int client_serialnumber, DateTime client_date, string client_registration)
        {
            using (_context)
            {
                var client = new Client
                {
                    Name = client_name,
                    Surname = client_surname,
                    SerialNumber = client_serialnumber,
                    Birthdate = client_date,
                    Registration = client_registration
                };
                _context.Clients.Add(client);
                int clientId = _context.Clients.Where(x => x.SerialNumber == client_serialnumber).FirstOrDefault().Id;
                var clientCard = new ClientCard
                {
                    ClientId = clientId,
                };
                _context.ClientCards.Add(clientCard);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
