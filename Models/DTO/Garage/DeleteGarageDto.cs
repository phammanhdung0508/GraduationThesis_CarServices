using System.ComponentModel;

namespace GraduationThesis_CarServices.Models.DTO.Garage
{
    public class DeleteGarageDto
    {
        public int GarageId { get; set; }
        [DefaultValue("false")]
        public bool GarageStatus { get; set; }
    }
}