#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class Mechanic
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MechanicId { get; set; }
        public int TotalWorkingHours { get; set; }

        /*-------------------------------------------------*/
        public virtual User User { get; set; }

        /*-------------------------------------------------*/
        public virtual ICollection<WorkingSchedule> WorkingSchedules { get; set; }
    }
}