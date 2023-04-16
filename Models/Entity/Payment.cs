#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarServices.Models.Entity
{
    public class Payment{
        public int payment_id { get; set; }
        public string payment_amount {get; set;}
        public string payment_method {get; set;}
        public bool payment_status {get; set;}
        public string payment_message {get; set;}
        public string currency {get; set;}

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}