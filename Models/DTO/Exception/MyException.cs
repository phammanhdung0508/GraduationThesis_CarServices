namespace GraduationThesis_CarServices.Models.DTO.Exception
{
    public class MyException : System.Exception
{
    public int StatusCode { get; }

    public MyException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}
}