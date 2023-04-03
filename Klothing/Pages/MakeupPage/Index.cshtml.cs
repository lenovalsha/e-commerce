using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Klothing.Data;
using Klothing.Models;
using System.ComponentModel;
using System.Collections.Immutable;
using MessagePack.Formatters;

namespace Klothing.Pages.MakeupPage
{
    public class IndexModel : PageModel
    {
        private readonly Klothing.Data.ApplicationDbContext _context;

        public IndexModel(Klothing.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Makeup> Makeup { get; set; } = default!;


        public async Task OnGetAsync()
        {
            if (_context.Makeups != null)
            {
                Makeup = await _context.Makeups
                .Include(m => m.Category).ToListAsync();
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {

            if (_context.Categories != null)
            {
                Makeup = await _context.Makeups.ToListAsync();
            }
            string[] blushes = { "The Patrick TA Blush", "Rare Beauty By Selena G", "NARS Blush", "Mario Blush", "Blush Balm" };
            foreach (string blush in blushes)
            {
                Makeup makeup = new Makeup { Name = blush };
                makeup.CategoryId = 1;
                _context.Makeups.Add(makeup);
            }
            string[] lipsticks = { "Locked Kiss Lipstick", "Cream Lipstain", "Forget the filler" };
            foreach (string lipstick in lipsticks)
            {
                Makeup makeup = new Makeup { Name = lipstick };
                makeup.CategoryId = 2;
                _context.Makeups.Add(makeup);
            }
            string[] foundations = { "SALE Foundation", "NARS Foundation", "NARS Foundation 2.0", "Haus Labs" };
            foreach (string foundation in foundations)
            {
                Makeup makeup = new Makeup { Name = foundation };
                makeup.CategoryId = 3;
                _context.Makeups.Add(makeup);
            }
            await _context.SaveChangesAsync();

            Makeup makeups = new Makeup();
            for (int i = 1; i < 6; i++)
            {
                for (int x = 1; x < 4; x++)
                {
                    Products products = new Products();
                    products.MakeupId = i;

                    products.Name = blushes[i-1] + x;
                    products.Description = "This is " + products.Name;
                    products.Image = "b" + i + $" ({x}).jpg";
                    products.QuantityInStock = 12;
                    products.Price = 24.99m;
                    _context.Products.Add(products);

                }
            }
            for (int i = 1; i < 4; i++)
            {
                for (int x = 1; x < 4; x++)
                {
                    Products products = new Products();
                    products.MakeupId = i+5;
                    products.Name = lipsticks[i - 1] + x;
                    products.Description = "This is " + products.Name;
                    products.Image = "l" + i + $" ({x}).jpg";
                    products.QuantityInStock = 12;
                    products.Price = 12.99m;
                    _context.Products.Add(products);

                }
            }
            for (int i = 1; i < 4; i++)
            {
                for (int x = 1; x < 4; x++)
                {
                    Products products = new Products();
                    products.MakeupId = i + 8;
                    products.Name = foundations[i - 1] + x;
                    products.Description = "This is " + products.Name;
                    products.Image = "f" + i + $" ({x}).jpg";
                    products.QuantityInStock = 12;
                    products.Price = 42.99m;
                    _context.Products.Add(products);

                }
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
