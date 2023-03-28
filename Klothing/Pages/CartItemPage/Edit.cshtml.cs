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

namespace Klothing.Pages.CartItemPage
{
    public class EditModel : PageModel
    {
        private readonly Klothing.Data.ApplicationDbContext _context;

        public EditModel(Klothing.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CartItem CartItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.CartItem == null)
            {
                return NotFound();
            }

            var cartitem =  await _context.CartItem.FirstOrDefaultAsync(m => m.Id == id);
            if (cartitem == null)
            {
                return NotFound();
            }
            CartItem = cartitem;
           ViewData["CartId"] = new SelectList(_context.Carts, "Id", "Id");
           ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
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

            _context.Attach(CartItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartItemExists(CartItem.Id))
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

        private bool CartItemExists(int id)
        {
          return (_context.CartItem?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
