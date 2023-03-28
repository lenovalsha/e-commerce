using Klothing.Models;

namespace Klothing.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }


        public List<OrderDetail> Details { get; set; }
        public Order()
        {
            Details = new List<OrderDetail>();
        }


    }
}
