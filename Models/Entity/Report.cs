#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class Report
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int report_id { get; set; }
        [Column(TypeName="Date")]
        public DateTime date { get; set; }
        [MaxLength(30)]
        public string notes { get; set; }
        [MaxLength(100)]
        public string description { get; set; }
        public bool report_status { get; set; }
        public DateTime created_at { get; set; }
        public DateTime update_at { get; set; }

        // public int booking_id { get; set; }
        public virtual Booking Booking { get; set; }
    }
}