using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.DTO.Payment
{
    public class DeletePaymentDto
    {
        public int PaymentId { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}