#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarServices.Models.Entity
{
    public class BookingDetail
    {
        public int booking_details_id { get; set; }

        public int booking_id { get; set; }
        public virtual Booking Booking { get; set; }
        public int schedule_id { get; set; }
        public virtual Schedule Schedule { get; set; }

        public virtual ICollection<Report> Reports { get; set; }
    }
}