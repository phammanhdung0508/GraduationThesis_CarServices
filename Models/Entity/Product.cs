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
        [Range(0, double.MaxValue, ErrorMessage = "")]
        public double ProductPrice { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "")]
        public int ProductQuantity { get; set; }
        // [Range(0, int.MaxValue, ErrorMessage = "")]
        // public int ProductSold { get; set; }
        [Column(TypeName = "tinyint")]
        public Status ProductStatus { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }

        /*-------------------------------------------------*/
        public Nullable<int> CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public Nullable<int> ServiceId { get; set; }
        public virtual Service Service { get; set; }

        /*-------------------------------------------------*/
        public virtual ICollection<BookingDetail> BookingDetails { get; set; }
    }
}