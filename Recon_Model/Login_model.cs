using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Recon_Model
{
   public  class Login_model
    {
        public string user_id { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public string datasource { get; set; }
        public string msg { get; set; }
        public string ip { get; set; }
        public string oldpassword { get; set; }

        public string ip_address { get; set; }
        public string user_code { get; set; }
        public Login_model()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST 
            ip_address = Dns.GetHostByName(hostName).AddressList[0].ToString();
        }
    }
}
