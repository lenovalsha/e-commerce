using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Klothing.Data;
using Klothing.Models;

namespace Klothing.Pages.CategoryPage
{
    public class IndexModel : PageModel
    {
        private readonly Klothing.Data.ApplicationDbContext _context;

        public IndexModel(Klothing.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Categories != null)
            {
                Category = await _context.Categories.ToListAsync();
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
           
            if (_context.Categories != null)
            {
                Category = await _context.Categories.ToListAsync();
            }
            string[] categories = { "Blush","Lipsticks","Foundations"};
            foreach(string category in categories)
            {
            Category categories1 = new Category { Name = category };
            _context.Categories.Add(categories1);
            }
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
