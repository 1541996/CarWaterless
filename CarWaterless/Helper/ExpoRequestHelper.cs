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
    public class ExpoRequestHelper
    {
        public static string sendTokenMessage(ExpoNotiViewModel data)
        {
            string response;

            //AIzaSyAzCVo8xuNAc3Keh5IYlsSLUyUQ1fFq9Ws
            //AIzaSyD0Ik8KiMVfKk2e0FBsYE0pCMCdXm8jFMY 
            //AIzaSyBXIrhOfJRvU-fRQK_T5G99-wlTgmS8qN4
            //

            try
            {

                WebRequest tRequest = WebRequest.Create("https://exp.host/--/api/v2/push/send");

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
              //  tRequest.Headers.Add("accept", "application/json");
                tRequest.Headers.Add("accept-encoding", "gzip, deflate");
             //   tRequest.Headers.Add("Content-Type", "application/json");
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
