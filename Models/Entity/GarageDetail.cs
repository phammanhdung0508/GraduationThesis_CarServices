#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class GarageDetail
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int GarageDetailId { get; set; }

        /*-------------------------------------------------*/
        public int? GarageId { get; set; }
        public virtual Garage Garage { get; set; }
        
        public int? ServiceId { get; set; }
        public virtual Service Service { get; set; }
    }
}