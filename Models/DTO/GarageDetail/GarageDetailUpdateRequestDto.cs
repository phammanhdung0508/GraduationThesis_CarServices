#nullable disable

namespace GraduationThesis_CarServices.Models.DTO.GarageDetail
{
    public class GarageDetailUpdateRequestDto
    {
        public int GarageDetailId  { get; set; }
        public int GarageId { get; set; }
        public int ServiceId { get; set; }     

    }
}