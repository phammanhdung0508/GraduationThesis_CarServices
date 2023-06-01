#nullable disable
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.DTO.WorkingSchedule
{
    public class WorkingScheduleCreateRequestDto
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string DaysOfTheWeek { get; set; }
        public string Description { get; set; }
        public int GarageId { get; set; }
        public int MechanicId { get; set; }
    }
}