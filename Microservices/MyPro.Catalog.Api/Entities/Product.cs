namespace MyPro.Catalog.Api.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int Category { get; set; }
        public decimal Rating { get; set; }
        public decimal Price { get; set; }
    }
}