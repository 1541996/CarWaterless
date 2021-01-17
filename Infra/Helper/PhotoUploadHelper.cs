using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using Microsoft.WindowsAzure.Storage;
//using Microsoft.Azure;
//using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace GoldChannelProject.helper
{
    public static class PhotoUploadHelper
    {
       
        public static string uploadPhoto(string stringInBase64, string path)
        {
            try
            {
                string imageName = Guid.NewGuid().ToString() + ".jpg";
                //set the image path
                string imgPath = Path.Combine(path, imageName);
                List<string> bs64List = stringInBase64.Split(',').ToList();
                if (bs64List.Count() > 0)
                {
                    stringInBase64 = bs64List[1];
                }
                byte[] imageBytes = Convert.FromBase64String(stringInBase64);
                System.IO.File.WriteAllBytes(imgPath, imageBytes);

                return imageName;
            }
            catch
            {
                return null;
            }
        }

        public static string uploadPhotoWithGif(string stringInBase64, string path)
        {
            try
            {
                string imageName = Guid.NewGuid().ToString() + ".gif";
                //set the image path
                string imgPath = Path.Combine(path, imageName);
                List<string> bs64List = stringInBase64.Split(',').ToList();
                if (bs64List.Count() > 0)
                {
                    stringInBase64 = bs64List[1];
                }
                byte[] imageBytes = Convert.FromBase64String(stringInBase64);
                System.IO.File.WriteAllBytes(imgPath, imageBytes);

                return imageName;
            }
            catch
            {
                return null;
            }
        }

       

    }
}