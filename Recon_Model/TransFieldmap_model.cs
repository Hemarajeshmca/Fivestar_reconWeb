using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Recon_Model
{
    public class TransFieldmap_model
    {
        public int slno { get; set; }       
        public string DBfieldName { get; set; }
        [Required(ErrorMessage = "Alias Should Not Be Blank")]
        public string Alias { get; set; }
        [Required(ErrorMessage = "Type Should Not Be Blank")]
        public string Type { get; set; }      
        public string Length { get; set; }
        public string Format { get; set; }
        public string Mandatory { get; set; }
        public string code { get; set; }
        public string Name { get; set; }
        public string dbsource { get; set; }       
        public int result { get; set; }
        public string msg { get; set; }
        public string temflag { get; set; }

        public string ip_address { get; set; }
        public string user_code { get; set; }
        public string temflag_dummy { get; set; }

        public TransFieldmap_model()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST 
            ip_address = Dns.GetHostByName(hostName).AddressList[0].ToString();
        }
    }
}
