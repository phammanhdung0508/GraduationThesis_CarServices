#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class Service
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ServiceId { get; set; }
        [MaxLength(100)]
        public string ServiceName { get; set; }
        [MaxLength(1024)]
        public string ServiceImage { get; set; }
        [MaxLength(200)]
        public string ServiceDetailDescription { get; set; }
        [MaxLength(20)]
        public int ServiceDuration { get; set; }
        [Column(TypeName = "tinyint")]
        public Status ServiceStatus {get; set;}
        public Nullable<DateTime> CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }

        /*-------------------------------------------------*/
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ServiceDetail> ServiceDetails { get; set; }
        public virtual ICollection<GarageDetail> GarageDetails { get; set; }
    }
}