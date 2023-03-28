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
    public class DeleteModel : PageModel
    {
        private readonly Klothing.Data.ApplicationDbContext _context;

        public DeleteModel(Klothing.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Makeup Makeup { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Makeups == null)
            {
                return NotFound();
            }

            var makeup = await _context.Makeups.FirstOrDefaultAsync(m => m.Id == id);

            if (makeup == null)
            {
                return NotFound();
            }
            else 
            {
                Makeup = makeup;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Makeups == null)
            {
                return NotFound();
            }
            var makeup = await _context.Makeups.FindAsync(id);

            if (makeup != null)
            {
                Makeup = makeup;
                _context.Makeups.Remove(Makeup);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
