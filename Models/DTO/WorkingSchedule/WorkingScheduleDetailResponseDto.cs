#nullable disable
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.DTO.WorkingSchedule
{
    public class WorkingScheduleDetailResponseDto
    {
        public int WorkingScheduleId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string DaysOfTheWeek { get; set; }
        public string Description { get; set; }
        public WorkingScheduleStatus WorkingScheduleStatus { get; set; }
        public virtual GarageWorkingScheduleDto GarageWorkingScheduleDto { get; set; }
        public virtual MechanicWorkingScheduleDto MechanicWorkingScheduleDto { get; set; }
    }
}