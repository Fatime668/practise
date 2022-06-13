using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project.DAL;
using project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project.Areas.ProjectAdmin.Controllers
{
    [Area("ProjectAdmin")]
    public class FeaturedController : Controller
    {
        private readonly AppDbContext _context;

        public FeaturedController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Featured> featureds = await _context.Featureds.ToListAsync();
            return View(featureds);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Featured featured)
        {
            if (!ModelState.IsValid) return View();
            await _context.AddAsync(featured);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int id)
        {
            Featured featured = await _context.Featureds.FirstOrDefaultAsync(c => c.Id == id);
            return View(featured);
        }
        public async Task<IActionResult> Edit(int id)
        {
            Featured featured = await _context.Featureds.FirstOrDefaultAsync(c => c.Id == id);
            if (featured == null) return NotFound();

            return View(featured);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int id,Featured featured)
        {
            Featured existedfeatured = await _context.Featureds.FirstOrDefaultAsync(c => c.Id == id);
            if (existedfeatured == null) return NotFound();

            if (existedfeatured.Id != id) return BadRequest();

            existedfeatured.Id = featured.Id;
            existedfeatured.Title = featured.Title;
            existedfeatured.Description = featured.Description;
            existedfeatured.Icon = featured.Icon;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Featured featured = await _context.Featureds.FirstOrDefaultAsync(c => c.Id == id);
            if (featured == null) return NotFound();
            return View(featured);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            Featured featured = await _context.Featureds.FirstOrDefaultAsync(c => c.Id == id);
            _context.Featureds.Remove(featured);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
