#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class BookingDetail
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int BookingDetailId { get; set; }
        [Range(0, double.MaxValue)]
        public double ProductCost { get; set; }
        [Range(0, double.MaxValue)]
        public double ServiceCost { get; set; }

        /*-------------------------------------------------*/
        public Nullable<int> BookingId { get; set; }
        public virtual Booking Booking { get; set; }

        public Nullable<int> ServiceDetailId { get; set; }
        public virtual ServiceDetail ServiceDetail { get; set; }

        public Nullable<int> ProductId { get; set; }
        public virtual Product Product { get; set; }
        
        public Nullable<int> MechanicId { get; set; }
        public virtual Mechanic Mechanic { get; set; }
    }
}