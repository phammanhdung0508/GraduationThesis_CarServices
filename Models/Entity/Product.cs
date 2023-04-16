#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarServices.Models.Entity
{
    public class Product
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int product_id { get; set; }
        [MaxLength(20)]
        public string product_name { get; set; }
        [MaxLength(200)]
        public string product_detail_description { get; set; }
        [Range(0, float.MaxValue, ErrorMessage = "")]
        public float product_price { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "")]
        public int product_quantity { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "")]
        public int product_sold { get; set; }
        public string product_status { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        // public int subcategory_id { get; set; }
        public virtual Subcategory Subcategory { get; set; }
        // public int service_id { get; set; }
        public virtual Service Service { get; set; }
    }
}