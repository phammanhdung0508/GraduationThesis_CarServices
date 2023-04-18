#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class Payment
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int payment_id { get; set; }
        [MaxLength(20)]
        public string payment_method { get; set; }
        public bool payment_status { get; set; }
        [MaxLength(100)]
        public string payment_message { get; set; }
        [MaxLength(5)]
        public string currency { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}