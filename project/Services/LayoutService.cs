using Microsoft.EntityFrameworkCore;
using project.DAL;
using project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project.Services
{
    public class LayoutService
    {
        private readonly AppDbContext _context;

        public LayoutService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Setting> GetDatas()
        {
            return await _context.Settings.FirstOrDefaultAsync();
        }
    }
}
