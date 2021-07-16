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
        public async static Task<string> sendTokenMessage(FCMViewModel data)
        {

            //string serverKey = "AAAAkHdeRik:APA91bHN9FuAb51GzU4cSfg1uTOwEKUspQcTzoXpGuH82I3yZtBOJJgda04RE8X4nozsQq-S5fSohA-E40CPWGaKSut5mM6JGSCqobhh8LTr_zfnis6NgOgCrskjVrZIi4_bgesNyYvR";
            //string senderId = "620477957673";


            string serverKey = "AAAAfuUQ_Pg:APA91bGZLvHCAWem7oK859sZKpPPLvvNYu12jy4ZnrpHoWjYv30TKNUu2BOLhUnLopqXb1U4BgosQRBg6-XA00q3MUZltj5Wwz-PvH7K5z57KuPMoGmf5Ik7_jOWzuvTfwet1ScMJnh_";
            string senderId = "545008975096";

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
