#nullable disable
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.DTO.WorkingSchedule
{
    public class WorkingScheduleUpdateStatusDto
    {
        public int WorkingScheduleId { get; set; }
        public WorkingScheduleStatus WorkingScheduleStatus { get; set; }
    }
}