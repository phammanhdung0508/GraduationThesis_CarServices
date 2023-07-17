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
        public int MinNumberOfCarLot { get; set; }
        public int MaxNumberOfCarLot { get; set; }
        [Column(TypeName = "decimal(10,3)")]
        public decimal ServicePrice { get; set; }

        /*-------------------------------------------------*/
        public int? ServiceId { get; set; }
        public virtual Service Service { get; set; }
        /*-------------------------------------------------*/
        public virtual ICollection<BookingDetail> BookingDetails { get; set; }
    }
}