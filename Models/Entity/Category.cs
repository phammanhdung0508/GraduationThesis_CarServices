#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class Category
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        [MaxLength(20)]
        public string CategoryName { get; set; }
        [Column(TypeName = "tinyint")]
        public Status CategoryStatus { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }

        /*-------------------------------------------------*/
        public virtual ICollection<Subcategory> Subcategories { get; set; }
    }
}