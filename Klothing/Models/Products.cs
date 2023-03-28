using Klothing.Models;

namespace Klothing.Models
{
    public class Products //this is basically the option
    {
        public int Id { get; set; }
        public string Name { get; set; }//option name
        public int MakeupId { get; set; }
        public Makeup? Makeup { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public string Image { get; set; }

        public List<OrderDetail>? Details { get; set; }
        public List<CartItem>? cartItems { get; set; }

        public Products()
        {
            Details = new List<OrderDetail>();
            cartItems = new List<CartItem>();
        }


    }
}
