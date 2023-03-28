using Klothing.Models;

namespace Klothing.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public string? Adress { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
                     
        public string? Country { get; set; }

        public List<Cart>? Carts { get; set; }

        public Customer()
        {
            Carts = new List<Cart>();
        }


    }
}
