using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Notification;
using Microsoft.AspNetCore.Mvc;
namespace project.Controllers;

[ApiController]
// [Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly FCMSendNotificationMobile fCMSendNotificationMobile;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, FCMSendNotificationMobile fCMSendNotificationMobile)
    {
        _logger = logger;
        this.fCMSendNotificationMobile = fCMSendNotificationMobile;
    }

    [HttpPost("test-notifi")]
    public async Task<IActionResult> DetailBooking(TestNotifi request)
    {
        await fCMSendNotificationMobile.SendMessagesToSpecificDevices
        (request.DeviceToken, "Thông báo:",
        $"Đơn hàng {request.BookingId}", request.BookingId);
        throw new MyException("Thành công.", 200);
    }

    // [HttpGet(Name = "GetWeatherForecast")]
    // public IEnumerable<WeatherForecast> Get()
    // {
    //     string[] names = new string[] { "name1", "name2", "name3" };
    //     Random rnd = new Random();
    //     int index = rnd.Next(names.Length);
    //     Console.WriteLine($"Name: {names[index]}");

    //     return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    //     {
    //         Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
    //         TemperatureC = Random.Shared.Next(-20, 55),
    //         Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    //     })
    //     .ToArray();
    // }
}