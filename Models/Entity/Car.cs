#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarServices.Models.Entity
{
    public class Car
    {
        public int car_id { get; set; }
        public string car_model { get; set; }
        public string car_make { get; set; }
        public string car_brand { get; set; }
        public string car_license_plate { get; set; }
        public string car_year { get; set; }
        public string car_body_type { get; set; }
        public string car_fuel_type { get; set; }

        public int user_id { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}