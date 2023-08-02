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
        [MaxLength(24680)]
        public string ServiceDetailDescription { get; set; }
        public int ServiceDuration { get; set; }
        [MaxLength(1024)]
        public string ServiceGroup { get; set; }
        [Column(TypeName = "tinyint")]
        public ServiceUnit ServiceUnit { get; set; }
        [Column(TypeName = "tinyint")]
        public Status ServiceStatus { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        /*-------------------------------------------------*/
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ServiceDetail> ServiceDetails { get; set; }
        public virtual ICollection<GarageDetail> GarageDetails { get; set; }
    }
}