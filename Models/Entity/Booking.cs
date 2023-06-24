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
        public string BookingCode {get; set;}
        public DateTime BookingTime { get; set; }
        public string PaymentMethod { get; set; }
        [Column(TypeName = "tinyint")]
        public PaymentStatus PaymentStatus { get; set; }
        [Column(TypeName = "tinyint")]
        public BookingStatus BookingStatus { get; set; }
        public double TotalPrice { get; set; }
        public int TotalEstimatedCompletionTime  { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }

        /*-------------------------------------------------*/
        public Nullable<int> CarId { get; set; }
        public virtual Car Car { get; set; }
        
        public Nullable<int> GarageId { get; set; }
        public virtual Garage Garage { get; set; }

        /*-------------------------------------------------*/
        public virtual ICollection<BookingDetail> BookingDetails { get; set; }
    }
}