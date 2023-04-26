using System.ComponentModel;

namespace GraduationThesis_CarServices.Models.DTO.Report
{
    public class DeleteReportDto
    {
        public int ReportId { get; set; }
        [DefaultValue("false")]
        public bool ReportStatus { get; set; }
    }
}