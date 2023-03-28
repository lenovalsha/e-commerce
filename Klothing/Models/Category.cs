namespace Klothing.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Makeup> Makeups { get; set; }
        public Category()
        {
            Makeups = new List<Makeup>();
        }
    }
}
