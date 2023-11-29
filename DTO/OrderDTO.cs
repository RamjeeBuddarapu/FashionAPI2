using FashionHexa.Entities;

namespace FashionHexa.DTO
{
    public class OrderDTO
    {
        public Guid OrderId { get; set; }
        public int ProductId { get; set; }
        public string? UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public Product? Product { get; set; }
        public User? User { get; set; }
    }
}
