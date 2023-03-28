using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Klothing.Data;
using Klothing.Models;

namespace Klothing.Pages.StatusPage
{
    public class DeleteModel : PageModel
    {
        private readonly Klothing.Data.ApplicationDbContext _context;

        public DeleteModel(Klothing.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Status Status { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Status == null)
            {
                return NotFound();
            }

            var status = await _context.Status.FirstOrDefaultAsync(m => m.Id == id);

            if (status == null)
            {
                return NotFound();
            }
            else 
            {
                Status = status;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Status == null)
            {
                return NotFound();
            }
            var status = await _context.Status.FindAsync(id);

            if (status != null)
            {
                Status = status;
                _context.Status.Remove(Status);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
