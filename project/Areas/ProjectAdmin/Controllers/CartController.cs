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
    public class CartController : Controller
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Cart> carts = await _context.Carts.ToListAsync();
            return View(carts);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Cart cart)
        {
            if (!ModelState.IsValid) return View();
            await _context.AddAsync(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int id)
        {
            Cart carts = await _context.Carts.FirstOrDefaultAsync(c => c.Id == id);
            return View(carts);
        }
        public async Task<IActionResult> Edit(int id)
        {
            Cart cart = await _context.Carts.FirstOrDefaultAsync(c => c.Id == id);
            if (cart == null) return NotFound();
            
            return View(cart);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int id, Cart cart)
        {
            Cart existedcart = await _context.Carts.FirstOrDefaultAsync(c => c.Id == id);
            if (existedcart == null) return NotFound();
            
            if (existedcart.Id != id) return BadRequest();

            existedcart.Id = cart.Id;
            existedcart.Title= cart.Title;
            existedcart.Description = cart.Description ;
            existedcart.IconUrl = cart.IconUrl;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Cart cart = await _context.Carts.FirstOrDefaultAsync(c => c.Id == id);
            if (cart == null) return NotFound();
            return View(cart);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            Cart cart = await _context.Carts.FirstOrDefaultAsync(c => c.Id == id);
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
