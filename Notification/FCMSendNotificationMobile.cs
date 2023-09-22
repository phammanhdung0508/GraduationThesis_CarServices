using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using FirebaseAdmin;
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

        public async Task SendMessagesToSpecificDevices(string deviceToken, string title, string body, int bookingId)
        {
            try
            {
                if (deviceToken is null)
                {
                    throw new MyException("Device Token đang rỗng", 410);
                }

                var message = new FirebaseAdmin.Messaging.Message
                {
                    Data = new Dictionary<string, string>
                    {
                        {"BookingId", bookingId != 0 ? bookingId.ToString() : "0"}
                    },
                    Notification = new FirebaseAdmin.Messaging.Notification()
                    {
                        Title = title,
                        Body = body
                    },
                    Token = deviceToken,
                };

                var firebaseInstance = FirebaseMessaging.GetMessaging(FirebaseApp.DefaultInstance);
                // Send a message to the device corresponding to the provided
                // registration token.
                string response = await firebaseInstance.SendAsync(message).ConfigureAwait(false);
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