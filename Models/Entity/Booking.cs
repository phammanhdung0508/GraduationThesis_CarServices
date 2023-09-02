#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class Booking
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int BookingId { get; set; }
        public string BookingCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public DateTime BookingTime { get; set; }
        [Column(TypeName = "decimal(10,3)")]
        public decimal OriginalPrice { get; set; } = 0;
        [Column(TypeName = "decimal(10,3)")]
        public decimal DiscountPrice { get; set; } = 0;
        [Column(TypeName = "decimal(10,3)")]
        public decimal TotalPrice { get; set; }
        [Column(TypeName = "decimal(10,3)")]
        public decimal FinalPrice { get; set; }
        public bool? IsAccepted {get; set;}
        public int TotalEstimatedCompletionTime { get; set; }
        public int CustomersCanReceiveTheCarTime { get; set; }
        [Column(TypeName = "tinyint")]
        public PaymentStatus PaymentStatus { get; set; }
        [Column(TypeName = "tinyint")]
        public BookingStatus BookingStatus { get; set; }
        public string QrImage { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        /*-------------------------------------------------*/
        public int? CarId { get; set; }
        public virtual Car Car { get; set; }

        public int? GarageId { get; set; }
        public virtual Garage Garage { get; set; }

        /*-------------------------------------------------*/
        public virtual ICollection<BookingDetail> BookingDetails { get; set; }
        public virtual ICollection<BookingMechanic> BookingMechanics { get; set; }
    }
}