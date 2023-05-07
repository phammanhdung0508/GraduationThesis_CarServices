#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class Review
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ReviewId { get; set; }
        [Range(0, 5, ErrorMessage = "")]
        public int Rating { get; set; }
        [MaxLength(1200)]
        public string Content { get; set; }
        [Column(TypeName = "tinyint")]
        public int ReviewStatus {get; set;}
        public Nullable<DateTime> CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public bool IsApproved { get; set; }

        /*-------------------------------------------------*/
        public Nullable<int> UserId { get; set; }
        public virtual User User { get; set; }
        public Nullable<int> GarageId { get; set; }
        public virtual Garage Garage { get; set; }
    }
}