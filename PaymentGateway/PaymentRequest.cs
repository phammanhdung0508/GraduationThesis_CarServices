using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationThesis_CarServices.PaymentGateway
{
    public class PaymentRequest
    {
        public int BookingId { get; set; }
        public int CarId { get; set; }
        public string PaymentId { get; set; } = string.Empty;
        public string PaymentContent { get; set; } = string.Empty;
        public string PaymentRefId { get; set; } = string.Empty;
        public decimal? RequiredAmount { get; set; }
        public DateTime? PaymnetDate { get; set; } = DateTime.Now;
        public DateTime? ExpireDate { get; set; } = DateTime.Now.AddMinutes(15);
        //public string? PaymentStatus {get; set;} = string.Empty;
        //public string? MerchantId {get; set;} = string.Empty;
        //public string? PaymentDestinationId {get; set;} = string.Empty;
        // public decimal? PaidAmount {get; set;}
        // public string? PaymentLastMessage {get; set;} = string.Empty;
    }
}