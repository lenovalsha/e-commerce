using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Klothing.Data;
using Klothing.Models;

namespace Klothing.Pages.MakeupPage
{
    public class CreateModel : PageModel
    {
        private readonly Klothing.Data.ApplicationDbContext _context;

        public CreateModel(Klothing.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Makeup Makeup { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Makeups == null || Makeup == null)
            {
                return Page();
            }

            _context.Makeups.Add(Makeup);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
