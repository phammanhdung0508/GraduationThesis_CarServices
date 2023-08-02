#nullable disable
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.DTO.WorkingSchedule
{
    public class WorkingScheduleByGarageDto
    {
        public int WorkingScheduleId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string DaysOfTheWeek { get; set; }
        public string WorkingScheduleStatus { get; set; }
        public virtual MechanicWorkingScheduleDto MechanicWorkingScheduleDto { get; set; }
    }
}