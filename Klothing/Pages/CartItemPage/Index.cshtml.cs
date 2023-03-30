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
    public class IndexModel : PageModel
    {
        private readonly Klothing.Data.ApplicationDbContext _context;

        public IndexModel(Klothing.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<CartItem> CartItem { get;set; } = default!;

        public async Task<IActionResult> OnGet()
        {
            var customerId = HttpContext.Session.GetInt32("customerId");
            var cartId = HttpContext.Session.GetInt32("cartId");

            if (customerId == null)
            {
                return Redirect("/Identity/Account/Login");
            }
            var Cart = _context.Carts.FirstOrDefault(m => m.Id == cartId);
            if (Cart == null)
                return Page();
            //int? cartId = Cart.Id;
            if (_context.CartItem != null)
            {
                CartItem = await _context.CartItem.Where(c => c.CartId == cartId)
                .Include(c => c.Cart)
                .Include(c => c.Product).ToListAsync();
            }
            return Page();
        }
    }
}
