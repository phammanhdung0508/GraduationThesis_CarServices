#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class Service
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int service_id { get; set; }
        [MaxLength(20)]
        public string service_name { get; set; }
        [MaxLength(200)]
        public string service_detail_description { get; set; }
        [Range(0, float.MaxValue, ErrorMessage = "")]
        public float product_price { get; set; }
        [MaxLength(6)]
        public string service_duration { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ServiceBooking> ServiceBookings { get; set; }
        public virtual ICollection<ServiceGarage> ServiceGarages { get; set; }
    }
}