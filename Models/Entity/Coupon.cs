#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class Coupon
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int coupon_id { get; set; }
        [MaxLength(20)]
        public string coupon_code { get; set; }
        [MaxLength(20)]
        public string coupon_type { get; set; }
        [Range(0, float.MaxValue, ErrorMessage = "")]
        public float coupon_value { get; set; }
        [Column(TypeName="Date")]
        public DateTime coupon_start_date { get; set; }
        [Column(TypeName="Date")]
        public DateTime coupon_end_date { get; set; }
        [Range(0, float.MaxValue, ErrorMessage = "")]
        public float coupon_min_spend { get; set; }
        [Range(0, float.MaxValue, ErrorMessage = "")]
        public float coupon_max_spend { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "")]
        public int number_of_times_to_use { get; set; }
        public bool coupon_status { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}