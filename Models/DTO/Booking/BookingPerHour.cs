#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class BookingPerHour
    {
        public string Hour {get; set;}
        public bool IsAvailable {get; set;} = true;
        public int EstimatedTimeCanBeBook {get; set;} = 0;
    }
}