using Klothing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            int cartId;
            //make a cart at the same time make a cart item
            int? customerId = HttpContext.Session.GetInt32("customerId");
            if(customerId== null)
            {
                return Redirect("/Identity/Account/Login");
            }
            //fint their cart       
            var Cart = _context.Carts.FirstOrDefault(m=> m.CustomerId == customerId);
            var product = _context.Products.FirstOrDefault(m => m.Id == productId);
            //find a cartitem that has this productId already
            cartId = Cart.Id;           
            var cartExist = _context.CartItem.FirstOrDefault(m => m.ProductId == productId && m.CartId == cartId && m.Cart.IsActive == true);
            if(cartExist == null)//if it doesnt exist then make a new one
            {
            //create the cartItem
            CartItem cartItem = new CartItem(); 
            cartItem.ProductId = productId;
            cartItem.CartId = cartId;
            cartItem.Quantity = 1;
            cartItem.IsActive = true;
            cartItem.Price = product.Price;
            _context.CartItem.Add(cartItem);
            await _context.SaveChangesAsync();

            }
            else //else update the quantity
            {
                cartExist.Quantity++;
                _context.Attach(cartExist).State = EntityState.Modified;
                _context.SaveChangesAsync();
            }

            return Redirect("/CartItemPage/Index");
        }

        [BindProperty]
        public CartItem CartItem { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
      
    }
}
