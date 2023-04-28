using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Payment;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface IPaymentRepository
    {
        Task<List<PaymentDto>?> View(PageDto page);
        Task<PaymentDto?> Detail(int id);
        Task Create(CreatePaymentDto paymentDto);
        Task Update(UpdatePaymentDto paymentDto);
        Task Delete(DeletePaymentDto paymentDto);
    }
}