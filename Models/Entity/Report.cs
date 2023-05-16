#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class Report
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ReportId { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        [MaxLength(800)]
        public string Notes { get; set; }
        [MaxLength(1200)]
        public string Description { get; set; }
        [Column(TypeName = "tinyint")]
        public Status ReportStatus { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }

        /*-------------------------------------------------*/
        public virtual Booking Booking { get; set; }
    }
}