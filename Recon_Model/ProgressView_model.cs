using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Recon_Model
{
   public class ProgressView_model
    {
        public int process_gid { get; set; }
        public string ProcessName { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string InitiatedBy { get; set; }
        public string process_status { get; set; }
        public string process_status_desc { get; set; }
        public string ip_addr { get; set; }      

        public string period_from { get; set; }
        public string period_to { get; set; }
        public string recongid { get; set; }
        public int recon_gid { get; set; }
        public string user_code { get; set; }
        public int result { get; set; }
        public string msg { get; set; }

        public string ip_address { get; set; }
        public ProgressView_model()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST 
            ip_address = Dns.GetHostByName(hostName).AddressList[0].ToString();
        }
    }
}
