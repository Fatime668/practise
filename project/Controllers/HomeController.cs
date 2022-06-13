using Microsoft.AspNetCore.Mvc;
using project.DAL;
using project.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeVM vm = new HomeVM {
                Sliders = _context.Sliders.ToList(),
                Featureds = _context.Featureds.ToList(),
                Carts = _context.Carts.ToList(),
                Clients = _context.Clients.ToList()
            };
            return View(vm);
        }
    }
}
