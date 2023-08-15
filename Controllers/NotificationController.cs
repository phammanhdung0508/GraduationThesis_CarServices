using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Notification;
using Microsoft.AspNetCore.Mvc;

namespace GraduationThesis_CarServices.Controllers
{
    [ApiController]
    [Route("api/notification")]
    public class NotificationController : ControllerBase
    {
        public readonly FCMSendNotificationMobile fCMSendNotificationMobile;
        public NotificationController(FCMSendNotificationMobile fCMSendNotificationMobile)
        {
            this.fCMSendNotificationMobile = fCMSendNotificationMobile;
        }

        [HttpPost("push-notification/{idToken}")]
        public async Task<IActionResult> PushNotification(string idToken)
        {
            await fCMSendNotificationMobile.SendMessagesToSpecificDevices(idToken);
            throw new MyException("Successfully.", 200);
        }
    }
}