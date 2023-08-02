#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class GarageMechanic
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int GarageMechanicId { get; set; }

        /*-------------------------------------------------*/
        public int? GarageId { get; set; }
        public virtual Garage Garage { get; set; }
        public int? MechanicId { get; set; }
        public virtual Mechanic Mechanic { get; set; }
    }
}