using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FashionHexa.Entities
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }
        public int ProductId { get; set; }
        [StringLength(5)] 
        [Column(TypeName = "char")]
        public string? UserId { get; set; }
        public DateTime OrderDate { get; set; }
        
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
