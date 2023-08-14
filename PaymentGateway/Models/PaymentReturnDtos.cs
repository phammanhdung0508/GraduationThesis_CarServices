namespace GraduationThesis_CarServices.PaymentGateway.Models
{
    public class PaymentReturnDtos
    {
        public string? PaymentId {get; set;}
        /// <summary>
        /// 00: Success
        /// 99: Unknown
        /// 10: Error
        /// </summary>
        public string? PaymentStatus {get; set;}
        public string? PaymentMessage {get; set;}
        /// <summary>
        /// Format: yyyyMMddHHmmss
        /// </summary>
        public string? PaymentDate {get; set;}
        public string? PaymentRefId {get; set;}
        public string? PaymentAmount {get; set;}
        public string? Signature {get; set;}
    }
}