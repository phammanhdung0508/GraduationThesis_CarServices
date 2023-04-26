#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class Product
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        [MaxLength(20)]
        public string ProductName { get; set; }
        [MaxLength(200)]
        public string ProductDetailDescription { get; set; }
        [Range(0, float.MaxValue, ErrorMessage = "")]
        public float ProductPrice { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "")]
        public int ProductQuantity { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "")]
        public int ProductSold { get; set; }
        public string ProductStatus { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }

        /*-------------------------------------------------*/
        public int SubcategoryId { get; set; }
        public virtual Subcategory Subcategory { get; set; }
        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }

        /*-------------------------------------------------*/
        public virtual ICollection<ProductMediaFile> ProductMediaFiles { get; set; }
    }
}