#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class Schedule
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ScheduleId { get; set; }
        public DateTime BookingTime { get; set; }
        [MaxLength(100)]
        public string WorkDescription { get; set; }
        [Column(TypeName = "tinyint")]
        public int ScheduleStatus { get; set; }

        /*-------------------------------------------------*/
        public int UserId { get; set; }
        public virtual User User { get; set; }

        /*-------------------------------------------------*/
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}