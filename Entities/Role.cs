using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FashionHexa.Entities
{
    [Table("Roles")]
    public class Role
    {
        [Key]
        [StringLength(5)]
        [Column(TypeName = "char")]
        public string? RoleId { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName ="varchar")]
        public string? RoleName { get; set;}
    }
}
