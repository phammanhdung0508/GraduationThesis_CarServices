#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Service
{
    public class UpdateServiceDto
    {
        public int service_id { get; set; }
        public string service_name { get; set; }
        public string service_detail_description { get; set; }
        public float product_price { get; set; }
        public string service_duration { get; set; }
    }
}