using FashionHexa.Entities;

namespace FashionHexa.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public double? Price { get; set; }
        public Brand? Brand { get; set; }
    }

}
