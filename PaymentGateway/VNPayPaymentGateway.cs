using System.Diagnostics;
using System.Reflection;
using AutoMapper;
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.PaymentGateway.Models;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GraduationThesis_CarServices.PaymentGateway
{
    public class VNPayPaymentGateway : IVNPayPaymentGateway
    {
        private readonly DataContext context;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public VNPayPaymentGateway(IConfiguration configuration, IMapper mapper,
        DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.configuration = configuration;
            this.mapper = mapper;
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<PaymentLinkDto> Create(PaymentRequest request)
        {
            try
            {
                var payment = mapper.Map<Payment>(request,
                obj => obj.AfterMap((src, des) =>
                {
                    des.PaymentId = request.PaymentId;
                    des.PaymentStatus = 0;
                }));

                context.Payments.Add(payment);
                await context.SaveChangesAsync();

                var response = GenerateVNPayURLPayment(payment);

                return response;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        private PaymentLinkDto GenerateVNPayURLPayment(Payment payment)
        {
            try
            {
                var vnp_returnUrl = configuration["VNPay:ReturnUrl"]!;
                var vnp_url = configuration["VNPay:Url"]!;
                var vnp_tmnCode = configuration["VNPay:TmnCode"]!;
                var vnp_hashSet = configuration["VNPay:HashSecret"]!;

                if (string.IsNullOrEmpty(vnp_tmnCode) || string.IsNullOrEmpty(vnp_hashSet))
                {
                    throw new MyException("Couldn't find TmnCode configurations and HashSecret configurations inside appsetting.json", 500);
                }

                string locale = configuration["VNPay:Locale"]!;
                var ipAddress = Utils.GetIpAddress(httpContextAccessor)!;
                //Build URL for VNPAY
                var vnpay = new VnPayLibrary();

                vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
                vnpay.AddRequestData("vnp_Command", "pay");
                vnpay.AddRequestData("vnp_TmnCode", vnp_tmnCode);
                vnpay.AddRequestData("vnp_Amount", (payment.RequiredAmount * 100).ToString()!);

                vnpay.AddRequestData("vnp_BankCode", "VNBANK");

                vnpay.AddRequestData("vnp_CreateDate", payment.PaymnetDate!.Value.ToString("yyyyMMddHHmmss"));
                vnpay.AddRequestData("vnp_CurrCode", "VND");
                vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(httpContextAccessor)!);
                vnpay.AddRequestData("vnp_Locale", locale);
                vnpay.AddRequestData("vnp_OrderInfo", payment.PaymentContent);
                vnpay.AddRequestData("vnp_OrderType", "other");
                vnpay.AddRequestData("vnp_ReturnUrl", vnp_returnUrl);
                vnpay.AddRequestData("vnp_TxnRef", payment.PaymentRefId);
                //vnpay.AddRequestData("vnp_ExpireDate", payment.ExpireDate!.Value.ToString("yyyyMMddHHmmss"));

                string paymentUrl = vnpay.CreateRequestUrl(vnp_url, vnp_hashSet);
                Console.WriteLine("VNPAY URL: {0}", paymentUrl);

                var response = new PaymentLinkDto()
                {
                    PaymentId = context.Payments.OrderByDescending(b => b.Id).Select(b => b.PaymentId).First(),
                    PaymentUrl = paymentUrl
                };

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<(/*PaymentReturnDtos*/bool, string)> CallVNPayReturnUrl(PaymentResponse request)
        {
            try
            {
                var isSuccess = false;

                var returnDto = new PaymentReturnDtos();
                var vnp_hashSet = configuration["VNPay:HashSecret"]!;
                var returnUrl = "https://mecarcustomer.page.link";
                var vnpay = new VnPayLibrary();

                var listProperties = typeof(PaymentResponse).GetProperties();

                foreach (PropertyInfo property in listProperties)
                {
                    var test = property.ToString(); //"System.String vnp_TmnCode"
                    var yet = property.Name; //"vnp_TmnCode"
                    var xet = property.PropertyType.Name; //"string"
                    var index = property.GetIndexParameters(); //{System.Reflection.ParameterInfo[0]}
                    var varlue = property.GetValue(request)!.ToString(); //"ABCDEXIA"

                    //get all querystring data
                    if (!string.IsNullOrEmpty(property.ToString()) &&
                    property.Name.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(property.Name, property.GetValue(request)!.ToString()!);
                    }
                }

                bool checkSignature = vnpay.ValidateSignature(request.vnp_SecureHash, vnp_hashSet);
                if (checkSignature)
                {
                    if (request.vnp_ResponseCode == "00" && request.vnp_TransactionStatus == "00")
                    {
                        /*returnDto.PaymentStatus = "00";
                        returnDto.PaymentMessage = "Giao dịch được thực hiện thành công. Cảm ơn quý khách đã sử dụng dịch vụ";
                        //Make new signature
                        returnDto.Signature = Guid.NewGuid().ToString();*/
                        var payment = await context.Payments.OrderByDescending(b => b.Id).FirstAsync();

                        var updateCar = await context.Cars.Where(c => c.CarId == payment.CarId)
                        .ExecuteUpdateAsync(s => s.SetProperty(c => c.CarBookingStatus, CarStatus.NotAvailable));

                        var updateBooking = await context.Bookings.Where(b => b.BookingId == payment.BookingId)
                        .ExecuteUpdateAsync(b => b.SetProperty(b => b.IsAccepted, true));

                        isSuccess = true;
                    }
                    else
                    {
                        //Thanh toan khong thanh cong. Ma loi: vnp_ResponseCode
                        /*returnDto.PaymentStatus = "10";
                        returnDto.PaymentMessage = $"Thanh toán không thành công. Mã lỗi: {request.vnp_ResponseCode}";*/
                        isSuccess = false;
                    }
                    /*returnDto.PaymentRefId = "Mã giao dịch thanh toán:" + request.vnp_TxnRef;
                    returnDto.PaymentAmount = "Số tiền thanh toán (VND):" + request.vnp_Amount.ToString();*/
                }
                else
                {
                    /*returnDto.PaymentStatus = "99";
                    returnDto.PaymentMessage = "Có lỗi xảy ra trong quá trình xử lý"; //Invalid signature*/
                    isSuccess = false;
                }

                /*return await Task.FromResult((returnDto, returnUrl));*/
                return await Task.FromResult((isSuccess, returnUrl));
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}