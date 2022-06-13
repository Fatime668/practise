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
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _context.Sliders.ToListAsync();
            return View(sliders);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (!ModelState.IsValid) return View();
            if (slider.Photo != null)
            {
                if (slider.Photo.IsOkay(1))
                {
                    ModelState.AddModelError("Photo", "Please, choose image file which size under 1 mb");
                    return View();
                }
                slider.Image = await slider.Photo.FileCreate(_env.WebRootPath, @"assets\image");
                await _context.AddAsync(slider);
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
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider == null) return NotFound();
            return View(slider);
        }
        public async Task<IActionResult> Edit(int id)
        {
            Slider slider =await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider == null) return NotFound();
            return View(slider);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int id,Slider slider)
        {
            Slider existedSlider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider.Photo != null)
            {
                if (!slider.Photo.IsOkay(1))
                {
                    string path = _env.WebRootPath + @"\assets\image"+ existedSlider.Image;
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    existedSlider.Image = await slider.Photo.FileCreate(_env.WebRootPath, @"assets\image");

                }
                else
                {
                    ModelState.AddModelError("Photo", "Selected image isn't valid");
                    return View(slider);
                }
            }
            existedSlider.Title = slider.Title;     
            existedSlider.Description = slider.Description;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider == null) return NotFound();
            return View(slider);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteSlider(int id)
        {
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider == null) return NotFound();
            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
