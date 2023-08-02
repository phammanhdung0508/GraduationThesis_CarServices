#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class Product
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        [MaxLength(100)]
        public string ProductName { get; set; }
        [MaxLength(1024)]
        public string ProductImage { get; set; }
        public ProductUnit ProductUnit {get; set;}
        [Column(TypeName = "decimal(10,3)")]
        public decimal ProductPrice { get; set; }
        [Range(0, 1000, ErrorMessage = "Out of range!")]
        public int ProductQuantity { get; set; }
        // [Range(0, int.MaxValue, ErrorMessage = "")]
        // public int ProductSold { get; set; }
        [Column(TypeName = "tinyint")]
        public Status ProductStatus { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        /*-------------------------------------------------*/
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int? ServiceId { get; set; }
        public virtual Service Service { get; set; }

        /*-------------------------------------------------*/
        public virtual ICollection<BookingDetail> BookingDetails { get; set; }
    }
}