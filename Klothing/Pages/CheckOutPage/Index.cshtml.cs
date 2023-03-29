using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Klothing.Data;
using Klothing.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Klothing.Pages.CheckOutPage
{
    public class IndexModel : PageModel
    {
        private readonly Klothing.Data.ApplicationDbContext _context;

        public IndexModel(Klothing.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var customerId = HttpContext.Session.GetInt32("customerId");
            if (customerId == null || _context.Orders == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FirstOrDefaultAsync(m => m.Id == customerId);
            if (customer == null)
            {
                return NotFound();
            }
            Customers = customer;

            return Page();
        }
        [BindProperty]
        public Customer Customers { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            int? cartId = HttpContext.Session.GetInt32("cartId");
            //if (!ModelState.IsValid) //UNDO THIS LATER FOR VALIDATION
            //{
            //    return Page();
            //}


            //lets update our customer
            _context.Attach(Customers).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            //user will click submit
            //now lets add an order
            Order order = new Order();
            order.CartId = cartId;
            order.OrderDate = DateTime.Now;
            order.StatusId = 2;
           _context.Orders.Add(order);
           await _context.SaveChangesAsync();
            

            //now lets make a orderdetails
            //get every products in the cart
            //foreach (var product in Car)
            var cartItems = await _context.CartItem.Where(m => m.CartId == cartId).ToListAsync();
            foreach (var cartItem in cartItems)
            {
                if (cartItem.IsActive)
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.OrderId = order.Id;
                    orderDetail.ProductId = cartItem.ProductId;
                    
                    orderDetail.Quantity = cartItem.Quantity;
                    orderDetail.Price = cartItem.Price;
                    //modify the cartitem and inactive it
                    cartItem.IsActive = false;
                    _context.Attach(cartItem).State = EntityState.Modified;
                    _context.OrderDetails.Add(orderDetail);
                    await   _context.SaveChangesAsync();

                }
            }
            //show the order details
            return RedirectToPage("/OrderDetailPage/Index", new {id = order.Id});
        }
    }
}
