using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Klothing.Data;
using Klothing.Models;

namespace Klothing.Pages.OrderDetailPage
{
    public class IndexModel : PageModel
    {
        private readonly Klothing.Data.ApplicationDbContext _context;

        public IndexModel(Klothing.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<OrderDetail> OrderDetail { get;set; } = default!;

        public async Task OnGetAsync(int? id)
        {
            if (_context.OrderDetails != null)
            {
                OrderDetail = await _context.OrderDetails.Where(m => m.OrderId == id)
                .Include(o => o.Order)
                .Include(o => o.Product).ToListAsync();
            }
        }
    }
}
