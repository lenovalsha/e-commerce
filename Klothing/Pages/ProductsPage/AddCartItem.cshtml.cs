using Klothing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.FileSystem;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Klothing.Pages.ProductsPage
{
    public class AddCartItemModel : PageModel
    {
        private readonly Klothing.Data.ApplicationDbContext _context;

        public AddCartItemModel(Klothing.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult>OnGetAsync(int? productId)
        {
            //variable
            //make a cart at the same time make a cart item
            int? customerId = HttpContext.Session.GetInt32("customerId");
            int? cartId = HttpContext.Session.GetInt32("cartId");
            if(customerId== null)
            {
                return Redirect("/Identity/Account/Login");
            }

            //find the active cart for this customer
            var activeCart = await _context.Carts.Include(c => c.cartItems).FirstOrDefaultAsync(c => c.CustomerId == customerId && c.IsActive);

            if(activeCart != null)
            {
                var cartItem = activeCart.cartItems.FirstOrDefault(c=>c.ProductId == productId);

                if(cartItem != null)//if an active cart item exist,let increment the quantity
                {
                    cartItem.Quantity++;
                    _context.Attach(cartItem).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }else
                {
                    var product = _context.Products.FirstOrDefault(m => m.Id == productId);
                    //create the cartItem
                    CartItem newCartItem = new CartItem();
                    newCartItem.ProductId = productId;
                    newCartItem.CartId = cartId;
                    newCartItem.Quantity = 1;
                    newCartItem.IsActive = true;
                    newCartItem.Price = product.Price;
                    _context.CartItem.Add(newCartItem);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                return Redirect("/Identity/Account/Login");
            }
           

            return Redirect("/CartItemPage/Index");
        }

        [BindProperty]
        public CartItem CartItem { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
      
    }
}
