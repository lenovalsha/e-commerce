using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Klothing.Data;
using Klothing.Models;

namespace Klothing.Pages.CartItemPage
{
    public class DetailsModel : PageModel
    {
        private readonly Klothing.Data.ApplicationDbContext _context;

        public DetailsModel(Klothing.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public CartItem CartItem { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.CartItem == null)
            {
                return NotFound();
            }

            var cartitem = await _context.CartItem.FirstOrDefaultAsync(m => m.Id == id);
            if (cartitem == null)
            {
                return NotFound();
            }
            else 
            {
                CartItem = cartitem;
            }
            return Page();
        }
    }
}
