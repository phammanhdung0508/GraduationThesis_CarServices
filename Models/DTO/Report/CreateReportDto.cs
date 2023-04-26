#nullable disable

namespace GraduationThesis_CarServices.Models.DTO.Report
{
    public class CreateReportDto
    {
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public string Description { get; set; }
        public bool ReportStatus { get; set; }
    }
}