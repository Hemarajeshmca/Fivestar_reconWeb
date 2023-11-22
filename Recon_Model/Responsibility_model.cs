using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Configuration;

namespace Recon_Model
{
    public class Responsibility_model
    {
        public int Respon_id { get; set; }
        [Required(ErrorMessage = "Responsibility Should Not Be Blank")]
        public string Respon_name { get; set; }
        public string Status { get; set; }
        public string Status_desc { get; set; }
        public string dbsource { get; set; }
        public int result { get; set; }
        public string msg { get; set; }

        public string ip_address { get; set; }
        public string user_code { get; set; }
        public Responsibility_model()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST 
            ip_address = Dns.GetHostByName(hostName).AddressList[0].ToString();
          
        }
    }
}
