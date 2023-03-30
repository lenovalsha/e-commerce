using Klothing.Models;
using NuGet.Repositories;

namespace Klothing.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public bool IsActive { get; set; }
        public List<Order>? Orders { get; set; }
        public List<CartItem>? cartItems { get; set; }
        public Cart()
        {
            Orders = new List<Order>();
            cartItems = new List<CartItem>();
        }
    }
}
