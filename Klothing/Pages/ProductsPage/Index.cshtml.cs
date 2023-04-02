using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Klothing.Data;
using Klothing.Models;

namespace Klothing.Pages.ProductsPage
{
    public class IndexModel : PageModel
    {
        private readonly Klothing.Data.ApplicationDbContext _context;

        public IndexModel(Klothing.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Products> Products { get; set; } = default!;
        public IList<Category> Categories { get; set; } = default!;


        public async Task OnGetAsync(string categoryId)
        {
            if (_context.Products != null)
            {
                var query = _context.Products
                .Include(p => p.Makeup).AsQueryable();
                if (!string.IsNullOrEmpty(categoryId))
                {
                    query = query.Where(p => p.Makeup.CategoryId.ToString() == categoryId);
                }
                Products = await query.ToListAsync();
            }
            if (_context.Categories != null)
            {
                Categories = await _context.Categories.ToListAsync();

            }
        }
            public async Task OnPostAsync(string categoryId)
            {
                if (_context.Products != null)
                {
                    var query = _context.Products
                    .Include(p => p.Makeup).AsQueryable();
                    if (!string.IsNullOrEmpty(categoryId))
                    {
                        query = query.Where(p => p.Makeup.CategoryId.ToString() == categoryId);
                    }
                    Products = await query.ToListAsync();
                }
                if (_context.Categories != null)
                {
                    Categories = await _context.Categories.ToListAsync();

                }
            }
        
    }
}
