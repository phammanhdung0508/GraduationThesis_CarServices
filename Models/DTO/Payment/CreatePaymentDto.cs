#nullable disable

namespace GraduationThesis_CarServices.Models.DTO.Payment
{
    public class CreatePaymentDto
    {
        public string PaymentMethod { get; set; }
        public string PaymentMessage { get; set; }
        public string Currency { get; set; }
    }
}