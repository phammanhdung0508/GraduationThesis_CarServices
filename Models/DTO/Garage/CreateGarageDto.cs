#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Garage
{
    public class CreateGarageDto
    {
        public string garage_name { get; set; }
        public string garage_contact_information { get; set; }
        public string garage_about { get; set; }
        public string garage_address { get; set; }
        public string garage_ward { get; set; }
        public string garage_district { get; set; }        
        public string garage_city { get; set; }
        public string from_to { get; set; }
        public string open_at { get; set; }
        public string close_at { get; set; }
    }
}