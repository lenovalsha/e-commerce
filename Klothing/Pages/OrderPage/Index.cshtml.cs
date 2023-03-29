using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Klothing.Data;
using Klothing.Models;

namespace Klothing.Pages.OrderPage
{
    public class IndexModel : PageModel
    {
        private readonly Klothing.Data.ApplicationDbContext _context;

        public IndexModel(Klothing.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            int? customer = HttpContext.Session.GetInt32("customerId");
            if (customer == null)
            {
                return Redirect("/Identity/Account/Login");
            }
            if (_context.Orders != null)
            {
                Order = await _context.Orders.Where(o=>o.Cart.CustomerId == customer)
                .Include(o => o.Cart)
                .Include(o => o.Status).ToListAsync();
            }
            return Page();
        }
    }
}
