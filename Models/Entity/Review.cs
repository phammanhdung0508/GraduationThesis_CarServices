#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class Review
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ReviewId { get; set; }
        [Range(0, 5)]
        public int Rating { get; set; }
        [MaxLength(2400)]
        public string Content { get; set; }
        [Column(TypeName = "tinyint")]
        public Status ReviewStatus {get; set;}
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        /*-------------------------------------------------*/
        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        
        public int? GarageId { get; set; }
        public virtual Garage Garage { get; set; }
    }
}