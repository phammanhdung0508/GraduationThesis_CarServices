#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class Subcategory
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int SubcategoryId { get; set; }
        [MaxLength(20)]
        public string SubcategoryName { get; set; }
        [Column(TypeName = "tinyint")]
        public Status SubcategoryStatus { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }

        /*-------------------------------------------------*/
        public Nullable<int> CategoryId { get; set; }
        public virtual Category Category { get; set; }

        /*-------------------------------------------------*/
        public virtual ICollection<Product> Products { get; set; }
    }
}