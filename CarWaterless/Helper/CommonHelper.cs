using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace CarWaterless.Helper
{
    public class CommonHelper
    {
        public static string downloadphoto(string poster)
        {
            if (poster != null && poster != "")
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead(poster);
                Bitmap bitmap; bitmap = new Bitmap(stream);
                string SigBase64 = "";
                try
                {
                    if (bitmap != null)
                    {
                        using (var ms = new MemoryStream())
                        {
                            using (var bitmapresult = new Bitmap(bitmap))
                            {
                                bitmapresult.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                SigBase64 = "data:image/jpeg;base64," + Convert.ToBase64String(ms.GetBuffer()); //Get Base64
                            }
                        }
                    }
                }
                catch { }
                stream.Flush();
                return SigBase64;
            }
            return null;
        }
    }
}