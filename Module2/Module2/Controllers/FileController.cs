using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Module2.Models;
using Module2.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Identity;

namespace Module2.Controllers
{
    public class FileController : Controller
    {
        private ApplicationDbContext _context;
        IHostingEnvironment _appEnvironment;
        private UserManager<ApplicationUser> _userManager;

        public FileController(ApplicationDbContext context, IHostingEnvironment appEnvironment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _appEnvironment = appEnvironment;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            ViewBag.CurrentUser = _userManager.GetUserId(User);
            return View(_context.Files.ToList());
        }
        public IActionResult Share()
        {
            return View();
        }
        public async Task<IActionResult> ShareFile(string email, string id)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var file = _context.Files.Where(x => x.Id.ToString() == id).FirstOrDefault();
                if (file.UserId == _userManager.GetUserId(User))
                {
                    if(file.SharedUserId==null)
                    {
                        file.SharedUserId = new List<ApplicationUser>();
                    }
                    file.SharedUserId.Add(user);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                string path = "/Files/" + uploadedFile.FileName;

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                FileModel file = new FileModel { Name = uploadedFile.FileName, Path = path, UserId = _userManager.GetUserId(User)};
                _context.Files.Add(file);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        public IActionResult Download(int id)
        {
            FileModel file = _context.Files.Where(x => x.Id == id).FirstOrDefault();
            if (file == null) return Content("error");
            var filepath = _appEnvironment.WebRootPath + file.Path;
            byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
            return File(fileBytes, "application/x-msdownload", file.Name);
        }
    }
}
