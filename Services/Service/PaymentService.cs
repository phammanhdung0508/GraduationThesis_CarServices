// using AutoMapper;
// using GraduationThesis_CarServices.Models.DTO.Page;
// using GraduationThesis_CarServices.Models.DTO.Payment;
// using GraduationThesis_CarServices.Repositories.IRepository;
// using GraduationThesis_CarServices.Services.IService;

// namespace GraduationThesis_CarServices.Services.Service
// {
//     public class PaymentService : IPaymentService
//     {
//         private readonly IPaymentRepository paymentRepository;
//         private readonly IMapper mapper;
//         public PaymentService(IPaymentRepository paymentRepository, IMapper mapper)
//         {
//             this.mapper = mapper;
//             this.paymentRepository = paymentRepository;
//         }

//         public async Task<List<PaymentDto>?> View(PageDto page)
//         {
//             try
//             {
//                 List<PaymentDto>? list = await paymentRepository.View(page);
//                 return list;
//             }
//             catch (Exception)
//             {
//                 throw;
//             }
//         }

//         public async Task<PaymentDto?> Detail(int id)
//         {
//             try
//             {
//                 PaymentDto? payment = mapper.Map<PaymentDto>(await paymentRepository.Detail(id));
//                 return payment;
//             }
//             catch (Exception)
//             {
//                 throw;
//             }
//         }

//         public async Task<bool> Create(CreatePaymentDto createPaymentDto)
//         {
//             try
//             {
//                 await paymentRepository.Create(createPaymentDto);
//                 return true;
//             }
//             catch (Exception)
//             {
//                 throw;
//             }
//         }

//         public async Task<bool> Update(UpdatePaymentDto updatePaymentDto)
//         {
//             try
//             {
//                 await paymentRepository.Update(updatePaymentDto);
//                 return true;
//             }
//             catch (Exception)
//             {
//                 throw;
//             }
//         }

//         public async Task<bool> Delete(DeletePaymentDto deletePaymentDto)
//         {
//             try
//             {
//                 await paymentRepository.Delete(deletePaymentDto);
//                 return true;
//             }
//             catch (Exception)
//             {
//                 throw;
//             }
//         }
//     }
// }