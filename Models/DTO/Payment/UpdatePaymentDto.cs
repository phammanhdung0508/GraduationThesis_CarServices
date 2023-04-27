#nullable disable

namespace GraduationThesis_CarServices.Models.DTO.Payment
{
    public class UpdatePaymentDto
    {
        public int PaymentId { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentMessage { get; set; }
        public string Currency { get; set; }
    }
}