using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.ViewModels
{
    public class FirebaseViewModel
    {
        public long secret_key { get; set; }


    }


    //public class FCMViewModel
    //{
    //    public string to { get; set; }
    //    public string priority { get; set; }
    //    public bool content_available { get; set; }
    //  //  public Notification notification { get; set; }
    //   // public DataModel data { get; set; }
    //}

    public class Notification
    {
        public string body { get; set; }
        public string title { get; set; }


    }

    public class FCMViewModel
    {
        public string to { get; set; }
        public string priority { get; set; }
        public fcmdata data { get; set; }
     //   public int time_to_live { get; set; }
    }

    public class fcmdata
    {
        public string title { get; set; }
        public string body { get; set; }
        public string type { get; set; }
        public string weburl { get; set; }
     
    }

}
