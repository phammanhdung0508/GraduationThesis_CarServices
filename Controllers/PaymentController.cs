using GraduationThesis_CarServices.PaymentGateway;
using GraduationThesis_CarServices.PaymentGateway.Models;
using Microsoft.AspNetCore.Mvc;

namespace GraduationThesis_CarServices.Controllers
{
    [ApiController]
    [Route("api/payment")]
    public class PaymentController : ControllerBase
    {
        private readonly IVNPayPaymentGateway vnpayPaymentGateway;
        public PaymentController(IVNPayPaymentGateway vnpayPaymentGateway)
        {
            this.vnpayPaymentGateway = vnpayPaymentGateway;
        }

        // /// <summary>
        // /// Create VNPay payment url for Customer.
        // /// </summary>
        // /// <param name="request"></param>
        // /// <returns></returns>
        // [HttpPost(("create-vnpay-payment-url"))]
        // public async Task<IActionResult> CreatePaymentUrl([FromBody] PaymentRequest request)
        // {
        //     var response = await vnpayPaymentGateway.Create(request);

        //     return Ok(response);
        // }

        [HttpGet]
        [Route("call-vnpay-return-url")]
        public async Task<IActionResult> CallReturnUrl([FromQuery] PaymentResponse response)
        {
            string returnUrl = string.Empty;
            string text = string.Empty;
            //var returnDto = new PaymentReturnDtos();
            var callUrl = await vnpayPaymentGateway.CallVNPayReturnUrl(response);

            if (/*callUrl.Item1 is not null &&*/
            callUrl.Item2 is not null)
            {
                //returnDto = callUrl.Item1;
                switch (callUrl.Item1)
                {
                    case true:
                        text = "success";
                        break;
                    case false:
                        text = "fail";
                        break;
                }
                returnUrl = callUrl.Item2!;
            }

            // if (returnUrl.EndsWith("/"))
            // {
            //     returnUrl = returnUrl.Remove(returnUrl.Length - 1, 1);
            // }

            return await Task.Run(() => Redirect($"{returnUrl}/{text}")); /*?{returnDto.ToQueryString()}*/
        }
    }
}