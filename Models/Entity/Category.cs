#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarServices.Models.Entity
{
    public class Category
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int category_id { get; set; }
        [MaxLength(20)]
        public string category_name { get; set; }
        public bool category_status { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        public virtual ICollection<Subcategory> Subcategories { get; set; }
    }
}