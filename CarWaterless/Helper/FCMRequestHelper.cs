using Infra.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Helper
{
    public class FCMRequestHelper
    {
        public static string sendTokenMessage(FCMViewModel data)
        {

            string serverKey = "AAAAG9Hjmu8:APA91bFOCean0f_XRTq6zquvpZPcgY92QM0E96eU5Q-ko9i9vOi3z4jbv7JkBMFYTMyu1BVC3aEA579wsMZtv9O9_2RbIbUOrAWTutuRne7q6w6M0BXsp5FamnHf-0GLX5MnLjzwveOX";
            string senderId = "119485471471";
            string response;

            //AIzaSyAzCVo8xuNAc3Keh5IYlsSLUyUQ1fFq9Ws
            //AIzaSyD0Ik8KiMVfKk2e0FBsYE0pCMCdXm8jFMY 
            //AIzaSyBXIrhOfJRvU-fRQK_T5G99-wlTgmS8qN4
            //

            try
            {

                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");

                tRequest.Method = "post";
                tRequest.ContentType = "application/json";


                //var data = new
                //{
                //    to = "/topics/GoldChannel_Message_Topic",
                //    priority = "high",
                //    content_available = true,
                //    notification = new
                //    {
                //        body = "Greetings",
                //        title = "Augsburg",

                //    },
                //    data = new
                //    {
                //        itemId = "1",
                //        type = "test",
                //        catId = "2",
                //    }

                //};


                var json = JsonConvert.SerializeObject(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;

                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                response = sResponseFromServer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = ex.Message;
            }

            return response;



        }
    }
}
