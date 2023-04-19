#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class Schedule
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int schedule_id { get; set; }
        public DateTime booking_time { get; set; }
        [MaxLength(100)]
        public string work_description { get; set; }
        public bool schedule_status { get; set; }

        // public int user_id { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}