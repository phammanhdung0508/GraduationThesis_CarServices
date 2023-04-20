using System.ComponentModel;

namespace GraduationThesis_CarServices.Models.DTO.Report
{
    public class DeleteReportDto
    {
        public int report_id { get; set; }
        [DefaultValue("false")]
        public bool report_status { get; set; }
    }
}