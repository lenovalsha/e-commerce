using Klothing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
            var _productId = productId;
            //fint their cart       
            var Cart = _context.Carts.FirstOrDefault(m=> m.CustomerId == customerId);
            cartId = Cart.Id;           
            //create the cartItem
            CartItem cartItem = new CartItem(); 
            cartItem.ProductId = _productId;
            cartItem.CartId = cartId;
            cartItem.Quantity = 1;
            cartItem.IsActive = true;
            _context.CartItem.Add(cartItem);
            await _context.SaveChangesAsync();
            return Page();
        }

        [BindProperty]
        public CartItem CartItem { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.CartItem == null || CartItem == null)
            {
                return Page();
            }

            _context.CartItem.Add(CartItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
