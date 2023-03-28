using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Klothing.Data;
using Klothing.Models;

namespace Klothing.Pages.ProductsPage
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
        ViewData["MakeupId"] = new SelectList(_context.Makeups, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Products Products { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Products == null || Products == null)
            {
                return Page();
            }

            _context.Products.Add(Products);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
