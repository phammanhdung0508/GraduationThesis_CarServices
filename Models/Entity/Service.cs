#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class Service
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ServiceId { get; set; }
        [MaxLength(20)]
        public string ServiceName { get; set; }
        [MaxLength(1024)]
        public string ServiceImage { get; set; }
        [MaxLength(200)]
        public string ServiceDetailDescription { get; set; }
        [Range(0, float.MaxValue, ErrorMessage = "")]
        public float ProductPrice { get; set; }
        [MaxLength(6)]
        public string ServiceDuration { get; set; }
        [Column(TypeName = "tinyint")]
        public int ServiceStatus {get; set;}
        public Nullable<DateTime> CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }

        /*-------------------------------------------------*/
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ServiceBooking> ServiceBookings { get; set; }
        public virtual ICollection<ServiceGarage> ServiceGarages { get; set; }
    }
}