using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Payment;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Paging;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GraduationThesis_CarServices.Repositories.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        public PaymentRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<PaymentDto>?> View(PageDto page)
        {
            try
            {
                List<Payment> list = await PagingConfiguration<Payment>.Get(context.Payments, page);
                return mapper.Map<List<PaymentDto>>(list);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PaymentDto?> Detail(int id)
        {
            try
            {
                PaymentDto payment = mapper.Map<PaymentDto>(await context.Payments.FirstOrDefaultAsync(c => c.PaymentId == id));
                return payment;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(CreatePaymentDto paymentDto)
        {
            try
            {
                Payment payment = mapper.Map<Payment>(paymentDto);
                context.Payments.Add(payment);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(UpdatePaymentDto paymentDto)
        {
            try
            {
                var payment = context.Payments.FirstOrDefault(c => c.PaymentId == paymentDto.PaymentId)!;
                mapper.Map<UpdatePaymentDto, Payment?>(paymentDto, payment);
                context.Payments.Update(payment);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Delete(DeletePaymentDto paymentDto)
        {
            try
            {
                var payment = context.Payments.FirstOrDefault(c => c.PaymentId == paymentDto.PaymentId)!;
                mapper.Map<DeletePaymentDto, Payment?>(paymentDto, payment);
                context.Payments.Update(payment);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}