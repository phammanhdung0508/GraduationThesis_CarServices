using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraduationThesis_CarServices.PaymentGateway.Models;

namespace GraduationThesis_CarServices.PaymentGateway
{
    public interface IVNPayPaymentGateway
    {
        Task<PaymentLinkDto> Create(PaymentRequest request);
        Task<(/*PaymentReturnDtos*/bool, string)> CallVNPayReturnUrl(PaymentResponse request);
    }
}