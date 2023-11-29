using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FashionHexa.Entities
{
    [Table("Sellers")]
    public class Seller
    {
        [Key]
        [Column(TypeName ="char")]
        [StringLength(5)]
        public string? SellerId { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName ="varchar")]
        public string SellerName { get; set;}
        [Required]
        [StringLength(50)]
        [Column("Email", TypeName = "varchar")]
        public string? Email { get; set; }
        [Required]
        [StringLength(50)]
        [Column("Phone", TypeName = "varchar")]
        public string? Mobile { get; set; }
        [Required]
        [StringLength(10)]
        [Column("Password", TypeName = "varchar")]
        public string? Password { get; set; }
        [ForeignKey("Role")]
        [StringLength(50)]
        public int RoleId { get; set; }
        public Role? Role { get; set; }


    }
}
