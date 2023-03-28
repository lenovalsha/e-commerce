namespace Klothing.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string TypeName { get; set; }
        public decimal Price { get; set; }
    }
}
