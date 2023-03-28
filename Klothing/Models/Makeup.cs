namespace Klothing.Models
{
    public class Makeup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public List<Products>? Products { get; set; }
        public Makeup()
        {
             Products = new List<Products>();
        }

    }
}
