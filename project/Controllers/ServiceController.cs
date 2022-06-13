using Microsoft.AspNetCore.Mvc;
using project.DAL;
using project.Models;
using project.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project.Controllers
{
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;

        public ServiceController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM vm = new HomeVM
            {
                Carts = _context.Carts.ToList()
            };

            return View(vm);
        }
    }
}
