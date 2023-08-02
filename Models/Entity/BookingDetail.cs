#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class BookingDetail
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int BookingDetailId { get; set; }
        [Column(TypeName = "decimal(10,3)")]
        public decimal ProductPrice { get; set; }
        [Column(TypeName = "decimal(10,3)")]
        public decimal ServicePrice { get; set; }
        [Column(TypeName = "tinyint")]
        public BookingServiceStatus BookingServiceStatus {get; set;}
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        /*-------------------------------------------------*/
        public int? BookingId { get; set; }
        public virtual Booking Booking { get; set; }

        public int? ServiceDetailId { get; set; }
        public virtual ServiceDetail ServiceDetail { get; set; }

        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}