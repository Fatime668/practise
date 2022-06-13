using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project.DAL;
using project.Models;
using project.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;

        public HeaderViewComponent(AppDbContext context)
        {
             _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Setting setting = await _context.Settings.FirstOrDefaultAsync();
            List<SocialMedia> socialMedias = await _context.SocialMedias.ToListAsync();
            HomeVM vm = new HomeVM 
            { 
                Settings = setting,
                SocialMedias = socialMedias
            };

            return View(vm);
        }
    }
}
