#nullable disable

using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.DTO.Report
{
    public class ReportDto
    {
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public string Description { get; set; }
        public ReportStatus ReportStatus { get; set; }
    }
}