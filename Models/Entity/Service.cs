#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarServices.Models.Entity
{
    public class Service
    {
        public int service_id { get; set; }
        public string service_name { get; set; }
        public string service_detail_description { get; set; }
        public string product_price { get; set; }
        public string service_duration { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ServiceBooking> ServiceBookings { get; set; }
        public virtual ICollection<ServiceGarage> ServiceGarages { get; set; }
    }
}