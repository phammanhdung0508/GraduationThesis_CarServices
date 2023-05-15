#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class WorkingSchedule
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int WorkingScheduleId { get; set; }
        [MaxLength(20)]
        public string StartTime { get; set; }
        [MaxLength(20)]
        public string EndTime { get; set; }
        [MaxLength(20)]
        public string DaysOfTheWeek { get; set; }
        [MaxLength(1200)]
        public string Description { get; set; }
        [Column(TypeName = "tinyint")]
        public WorkingScheduleStatus WorkingScheduleStatus { get; set; }

        /*-------------------------------------------------*/
        public Nullable<int> GarageId { get; set; }
        public virtual Garage Garage { get; set; }
        public Nullable<int> MechanicId { get; set; }
        public virtual Mechanic Mechanic { get; set; }
    }
}