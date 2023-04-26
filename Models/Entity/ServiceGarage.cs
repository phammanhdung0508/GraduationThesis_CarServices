#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class ServiceGarage
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ServiceGaragesId { get; set; }
        [Range(0, 100, ErrorMessage = "")]
        public int LotNumber { get; set; }
        [Column(TypeName = "tinyint")]
        public int LotStatus { get; set; }

        /*-------------------------------------------------*/
        public int GarageId { get; set; }
        public virtual Garage Garage { get; set; }
        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }
    }
}