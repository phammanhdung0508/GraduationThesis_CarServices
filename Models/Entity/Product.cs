#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarServices.Models.Entity
{
    public class Product
    {
        public int product_id { get; set; }
        public string product_name { get; set; }
        public string product_detail_description { get; set; }
        public string product_price { get; set; }
        public string service_name { get; set; }
        public string product_quantity { get; set; }
        public string product_status { get; set; }
        public string product_sold { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        public int subcategory_id { get; set; }
        public virtual Subcategory Subcategory { get; set; }
        public int service_id { get; set; }
        public virtual Service Service { get; set; }
    }
}