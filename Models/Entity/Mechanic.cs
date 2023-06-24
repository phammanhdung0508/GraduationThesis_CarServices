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
        public MechanicStatus MechanicStatus {get; set;}
        public int TotalWorkingHours { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }

        /*-------------------------------------------------*/
        public virtual User User { get; set; }

        /*-------------------------------------------------*/
        public virtual ICollection<GarageMechanic> GarageMechanics { get; set; }
        public virtual ICollection<BookingDetail> BookingDetails { get; set; }
    }
}