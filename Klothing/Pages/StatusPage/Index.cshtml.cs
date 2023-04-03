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
    public class IndexModel : PageModel
    {
        private readonly Klothing.Data.ApplicationDbContext _context;

        public IndexModel(Klothing.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Status> Status { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Status != null)
            {
                Status = await _context.Status.ToListAsync();
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (_context.Status != null)
            {
                Status = await _context.Status.ToListAsync();
            }
            string[] statuses = { "Payment Received", "In-Progress", "Shipped", "Completed" };
            foreach(string status in statuses)
            {
                Status s= new Status { Name = status };
                _context.Status.Add(s);
            }
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
