#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarServices.Models.Entity
{
    public class Schedule
    {
        public int schedule_id { get; set; }
        public DateTime created_at { get; set; }
        public DateOnly date { get; set; }
        public TimeOnly time { get; set; }
        public string work { get; set; }
        public bool schedule_status { get; set; }

        public int user_id { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<BookingDetail> BookingDetails { get; set; }
    }
}