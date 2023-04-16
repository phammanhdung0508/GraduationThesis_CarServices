#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarServices.Models.Entity
{
    public class Subcategory
    {
        public int subcategory_id { get; set; }
        public string subcategory_name { get; set; }
        public bool subcategory_status { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        public int category_id { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}