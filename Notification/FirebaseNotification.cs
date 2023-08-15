#nullable disable
namespace GraduationThesis_CarServices.Notification
{
    public class FirebaseNotification
    {
        public class Data{
            public string Body { get; set;}
            public string Title {get; set;}
            public string Key_1 {get; set;}
            public string Key_2 {get; set;}
        }

        public class Message{
            public string Token { get; set;}
            public Data Data { get; set;}
            public Notification Notification {get; set;}
        }

        public class Notification{
            public string Title { get; set;}
            public string Body { get; set;}
        }

        public class Root{
            public Message Message {get; set;}
        }
    }
}