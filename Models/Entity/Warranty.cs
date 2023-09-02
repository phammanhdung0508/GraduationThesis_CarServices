#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class Warranty
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int WarrantyId { get; set; }
        public DateTime WarrantyDate { get; set; }
        public string Reason { get; set; }

        /*-------------------------------------------------*/
        public int? ServiceId { get; set; }
        public virtual Service Service { get; set; }

        public int? BookingId { get; set; }
        public virtual Booking Booking { get; set; }
    }
}