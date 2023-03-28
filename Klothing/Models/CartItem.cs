using Klothing.Models;

namespace Klothing.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int? CartId { get; set; }
        public Cart? Cart { get; set; }
        public int? ProductId { get; set; }
        public Products? Product { get; set; }
        public int Quantity { get; set; }
        public bool IsActive { get; set; }
    }
}
