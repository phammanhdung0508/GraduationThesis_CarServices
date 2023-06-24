#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class ServiceDetail
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ServiceDetailId { get; set; }
        [Range(0, float.MaxValue, ErrorMessage = "")]
        public double ServicePrice { get; set; }
        public int MinNumberOfCarLot { get; set; }
        public int MaxNumberOfCarLot { get; set; }

        /*-------------------------------------------------*/
        public Nullable<int> ServiceId { get; set; }
        public virtual Service Service { get; set; }
        /*-------------------------------------------------*/
        public virtual ICollection<BookingDetail> BookingDetails { get; set; }
    }
}