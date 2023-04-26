#nullable disable

namespace GraduationThesis_CarServices.Models.DTO.Report
{
    public class UpdateReportDto
    {
        public int ReportId { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public string Description { get; set; }
        public bool ReportStatus { get; set; }
    }
}