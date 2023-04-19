using System.ComponentModel;

namespace GraduationThesis_CarServices.Models.DTO.Garage
{
    public class DeleteGarageDto
    {
        public int garage_id { get; set; }
        [DefaultValue("false")]
        public bool garage_status { get; set; }
    }
}