using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Klothing.Data;
using Klothing.Models;

namespace Klothing.Pages.MakeupPage
{
    public class IndexModel : PageModel
    {
        private readonly Klothing.Data.ApplicationDbContext _context;

        public IndexModel(Klothing.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Makeup> Makeup { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Makeups != null)
            {
                Makeup = await _context.Makeups
                .Include(m => m.Category).ToListAsync();
            }
        }
    }
}
