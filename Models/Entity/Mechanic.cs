#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class Mechanic
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MechanicId { get; set; }
        [Column(TypeName = "tinyint")]
        public MechanicStatus MechanicStatus {get; set;}
        [MaxLength(1024)]
        public string Level {get; set;}


        /*-------------------------------------------------*/
        public virtual User User { get; set; }

        /*-------------------------------------------------*/
        public virtual ICollection<BookingMechanic> BookingMechanics { get; set; }
        public virtual ICollection<GarageMechanic> GarageMechanics { get; set; }
    }
}