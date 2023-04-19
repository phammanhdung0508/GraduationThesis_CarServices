#nullable disable

namespace GraduationThesis_CarServices.Models.DTO.Report
{
    public class UpdateReportDto
    {
        public int report_id { get; set; }
        public DateTime date { get; set; }
        public string notes { get; set; }
        public string description { get; set; }
        public bool report_status { get; set; }
    }
}