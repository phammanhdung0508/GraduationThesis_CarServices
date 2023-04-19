#nullable disable

namespace GraduationThesis_CarServices.Models.DTO.User
{
    public class UpdateUserDto
    {
        public int user_id { get; set; }
        public string user_first_name { get; set; }
        public string user_last_name { get; set; }
        public string full_name { get; set; }
        public string user_email { get; set; }
        public string user_address { get; set; }
        public string user_city { get; set; }
        public string user_district { get; set; }
        public string user_ward { get; set; }
        public string user_phone { get; set; }
        public bool user_gender { get; set; }
        public DateTime user_date_of_birth { get; set; }
        public string user_image { get; set; }
        public string user_bio { get; set; }
    }
}