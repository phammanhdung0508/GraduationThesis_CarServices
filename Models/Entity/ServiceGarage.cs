#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class ServiceGarage
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int service_garages_id { get; set; }
        [Range(0, 200, ErrorMessage = "")]
        public int lot_number { get; set; }
        public bool lot_status { get; set; }

        // public int garage_id { get; set; }
        public virtual Garage Garage { get; set; }
        // public int service_id { get; set; }
        public virtual Service Service { get; set; }
    }
}