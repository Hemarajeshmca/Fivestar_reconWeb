using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Recon_Model
{
   public class Common_model
    {
        public string field_value { get; set; }
        public int filter_flag { get; set; }
        public string ip_address { get; set; }
        public string user_code { get; set; }
        public string result_value { get; set; }
        public string msg { get; set; }
        public int result { get; set; }
        public Common_model()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST 
            ip_address = Dns.GetHostByName(hostName).AddressList[0].ToString();
        }
    }
}
