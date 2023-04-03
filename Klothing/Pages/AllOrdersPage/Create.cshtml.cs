using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Klothing.Data;
using Klothing.Models;

namespace Klothing.Pages.AllOrdersPage
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
        ViewData["CartId"] = new SelectList(_context.Carts, "Id", "Id");
        ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Order Order { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Orders == null || Order == null)
            {
                return Page();
            }

            _context.Orders.Add(Order);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
