#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarServices.Models.Entity
{
    public class Garage
    {
        public int garage_id { get; set; }
        public string garage_name { get; set; }
        public string garage_contact_information { get; set; }
        public TimeOnly open_at { get; set; }
        public TimeOnly close_at { get; set; }
        public string garage_address { get; set; }
        public bool garage_status { get; set; }
        public string garage_about { get; set; }

        public int user_id { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<ServiceGarage> ServiceGarages { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}