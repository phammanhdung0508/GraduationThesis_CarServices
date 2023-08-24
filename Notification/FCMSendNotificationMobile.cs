using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using GraduationThesis_CarServices.Models.DTO.Exception;
using Newtonsoft.Json;
using static GraduationThesis_CarServices.Notification.FirebaseNotification;
// using static GraduationThesis_CarServices.Notification.FirebaseNotification;

namespace GraduationThesis_CarServices.Notification
{
    public class FCMSendNotificationMobile
    {
        private readonly IConfiguration configuration;

        public FCMSendNotificationMobile(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendMessagesToSpecificDevices(string idToken)
        {
            try
            {
                // WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                // tRequest.Method = "post";
                // //serverKey - Key from Firebase cloud messaging server  
                // tRequest.Headers.Add(string.Format("Authorization: key={0}", "AAAAE3v0Ldo:APA91bFL5luRuRv5DPWHF94WKeQi20Ofju5j4YBys__uN329gCALYi6sr4gVBrXp0VM6w6qhJ2M-FAL383fjW9MP5g_hCESxo21Ep5D4FWhz2HFMlhiH_bRB3Sflq7rERcbqKv8JsCej"));
                // //Sender Id - From firebase project setting  
                // tRequest.Headers.Add(string.Format("Sender: id={0}", "83683978714"));
                // tRequest.ContentType = "application/json";
                // var payload = new
                // {
                //     to = "fx2pY9z_QfO-3LKXTzwo4y:APA91bHQELGe7352TO-YaRHB6BaaguO3h7CWEPhS5EAXfHIJqp9CAOxXK07jTGBEbU-iB2lg8pW5IrOhvWVcml7BncxwyEDSob9Y1ljk-hLJnDQzh2sHVahTb0qcH64kRKgR6a_bxHCp",
                //     priority = "high",
                //     content_available = true,
                //     notification = new
                //     {
                //         body = "Test body",
                //         title = "Test title",
                //         badge = 1
                //     },
                //     data = new
                //     {
                //         key1 = "value1",
                //         key2 = "value2"
                //     }

                // };

                // string postbody = JsonConvert.SerializeObject(payload).ToString();
                // Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
                // tRequest.ContentLength = byteArray.Length;
                // using (Stream dataStream = tRequest.GetRequestStream())
                // {
                //     dataStream.Write(byteArray, 0, byteArray.Length);
                //     using (WebResponse tResponse = tRequest.GetResponse())
                //     {
                //         using (Stream dataStreamResponse = tResponse.GetResponseStream())
                //         {
                //             if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                //                 {
                //                     String sResponseFromServer = tReader.ReadToEnd();
                //                     //result.Response = sResponseFromServer;
                //                 }
                //         }
                //     }
                // }

                var message = new FirebaseAdmin.Messaging.Message
                {
                    Data = new Dictionary<string, string>
                    {
                        {"random_data", "not string"},
                        {"hot_data", "also_not_string"}
                    },
                    Notification = new FirebaseAdmin.Messaging.Notification()
                    {
                        Title = "title",
                        Body = "body"
                    },
                    Token = idToken,
                };

                // Send a message to the device corresponding to the provided
                // registration token.
                //var firebaseInstance = FirebaseMessaging.GetMessaging();
                string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
                // Response is a message ID string.
                Console.WriteLine("Successfully sent message: " + response);
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

        public void GenerateFCM_Auth_SendNotifcn()
        {
            try
            {
                //Generating Bearer token for FCM
                string scopes = "https://www.googleapis.com/auth/firebase.messaging";
                var bearertoken = ""; // Bearer Token in this variable
                using (var stream = new FileStream(configuration["Firebase:GoogleCredential"]!, FileMode.Open, FileAccess.Read))
                {
                    bearertoken = GoogleCredential
                      .FromStream(stream) // Loads key file
                      .CreateScoped(scopes) // Gathers scopes requested
                      .UnderlyingCredential // Gets the credentials
                      .GetAccessTokenForRequestAsync().Result; // Gets the Access Token
                }

                //Calling FCM
                var clientHandler = new HttpClientHandler();
                var client = new HttpClient(clientHandler)
                {
                    BaseAddress = new Uri("https://fcm.googleapis.com/v1/projects/carservices-868c3-approval/messages:send") // FCM HttpV1 API
                };

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //client.DefaultRequestHeaders.Accept.Add("Authorization", "Bearer " + bearertoken);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearertoken); // Authorization Token in this variable

                //Assigning Of data To Model
                var rootObj = new Root
                {
                    Message = new FirebaseNotification.Message()
                };

                rootObj.Message.Token = ""; //FCM Token id

                rootObj.Message.Data = new Data
                {
                    Title = "Data Title",
                    Body = "Data Body",
                    Key_1 = "Sample Key",
                    Key_2 = "Sample Key2"
                };

                rootObj.Message.Notification = new FirebaseNotification.Notification
                {
                    Title = "Notify Title",
                    Body = "Notify Body"
                };

                //Convert Model To JSON

                string jsonObj = JsonConvert.SerializeObject(rootObj, Formatting.Indented);

                //Calling Of FCM Notify API
                var data = new StringContent(jsonObj, Encoding.UTF8, "application/json");
                data.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = client.PostAsync("https://fcm.googleapis.com/v1/projects/**-approval/messages:send", data).Result; // Calling The FCM httpv1 API

                //Deserialize Json Response from API

                var jsonResponse = response.Content.ReadAsStringAsync().Result;
                var responseObj = JsonConvert.DeserializeObject(jsonResponse);
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
    }
}