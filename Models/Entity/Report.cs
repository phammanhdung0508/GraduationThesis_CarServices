#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class Report
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ReportId { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        [MaxLength(30)]
        public string Notes { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
        [Column(TypeName = "tinyint")]
        public int ReportStatus { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }

        /*-------------------------------------------------*/
        public int BookingId { get; set; }
        public virtual Booking Booking { get; set; }
    }
}