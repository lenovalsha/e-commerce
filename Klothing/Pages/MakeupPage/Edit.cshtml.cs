using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Klothing.Data;
using Klothing.Models;

namespace Klothing.Pages.MakeupPage
{
    public class EditModel : PageModel
    {
        private readonly Klothing.Data.ApplicationDbContext _context;

        public EditModel(Klothing.Data.ApplicationDbContext context)
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

            var makeup =  await _context.Makeups.FirstOrDefaultAsync(m => m.Id == id);
            if (makeup == null)
            {
                return NotFound();
            }
            Makeup = makeup;
           ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Makeup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MakeupExists(Makeup.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MakeupExists(int id)
        {
          return (_context.Makeups?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
