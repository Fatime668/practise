using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project.DAL;
using project.Extensions;
using project.Models;
using project.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project.Areas.ProjectAdmin.Controllers
{
    [Area("ProjectAdmin")]
    public class ClientController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ClientController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Client> client = await _context.Clients.ToListAsync();
            return View(client);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Client client)
        {
            if (!ModelState.IsValid) return View();
            if (client.Photo != null)
            {
                if (client.Photo.IsOkay(1))
                {
                    ModelState.AddModelError("Photo", "Please, choose image file which size under 1 mb");
                    return View();
                }
                client.Logo = await client.Photo.FileCreate(_env.WebRootPath, @"assets\image\brand");
                await _context.AddAsync(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            else
            {
                ModelState.AddModelError("Photo", "Please, choose file");
                return View();
            }
        }
        public async Task<IActionResult> Detail(int id)
        {
            Client client = await _context.Clients.FirstOrDefaultAsync(s => s.Id == id);
            if (client == null) return NotFound();
            return View(client);
        }
        public async Task<IActionResult> Edit(int id)
        {
            Client client = await _context.Clients.FirstOrDefaultAsync(s => s.Id == id);
            if (client == null) return NotFound();
            return View(client);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int id, Client client)
        {
            Client existedClient = await _context.Clients.FirstOrDefaultAsync(s => s.Id == id);
            if (client.Photo != null)
            {
                if (!client.Photo.IsOkay(1))
                {
                    string path = _env.WebRootPath + @"assets\image\brand" + existedClient.Logo;
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    existedClient.Logo = await client.Photo.FileCreate(_env.WebRootPath, @"assets\image\brand");

                }
                else
                {
                    ModelState.AddModelError("Photo", "Selected image isn't valid");
                    return View(client);
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Client client = await _context.Clients.FirstOrDefaultAsync(s => s.Id == id);
            if (client == null) return NotFound();
            return View(client);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteLogo(int id)
        {
            Client client = await _context.Clients.FirstOrDefaultAsync(s => s.Id == id);
            if (client == null) return NotFound();
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
