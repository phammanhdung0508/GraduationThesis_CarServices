#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarServices.Models.Entity
{
    public class Coupon
    {
        public int coupon_id { get; set; }
        public string coupon_code { get; set; }
        public string coupon_type { get; set; }
        public string coupon_value { get; set; }
        public DateOnly coupon_start_date { get; set; }
        public DateOnly coupon_end_date { get; set; }
        public float coupon_min_spend { get; set; }
        public float coupon_max_spend { get; set; }
        public int number_of_times_to_use { get; set; }
        public bool coupon_status { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}