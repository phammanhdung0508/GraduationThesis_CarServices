#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarServices.Models.Entity
{
    public class Report
    {
        public int report_id { get; set; }
        public DateTime created_at { get; set; }
        public string issue { get; set; }
        public string description { get; set; }
        public bool report_status { get; set; }

        public int booking_details_id { get; set; }
        public virtual BookingDetail BookingDetail { get; set; }
        public int garage_id { get; set; }
        public virtual Garage Garage { get; set; }
    }
}