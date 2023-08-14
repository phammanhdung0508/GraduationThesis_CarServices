#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class BookingMechanic
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int BookingMechanicId { get; set; }
        public DateTime WorkingDate { get; set; }
        public Status BookingMechanicStatus { get; set; }

        /*-------------------------------------------------*/
        public int? BookingId { get; set; }
        public virtual Booking Booking { get; set; }
        public int? MechanicId { get; set; }
        public virtual Mechanic Mechanic { get; set; }
    }
}