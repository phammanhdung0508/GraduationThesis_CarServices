#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.User
{
    public class CustomerDetailResponseDto
    {
        public string FullName { get; set; }
        public string UserPhone { get; set; }
        public string UserEmail { get; set; }
        public string CreatedAt { get; set; }
        public UserCustomerDto UserCustomerDto {get; set;}
    }
}