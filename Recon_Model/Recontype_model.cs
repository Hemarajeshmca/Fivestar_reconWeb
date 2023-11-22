using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Recon_Model
{
    public class Recontype_model
    {
        public int recon_id { get; set; }
        [Required(ErrorMessage = "Recon Name Should Not Be Blank")]
        public string ReconName { get; set; }
        public string Account1 { get; set; }
        public string Account2 { get; set; }
        public string MappingType_code { get; set; }
        public string MappingType_name { get; set; }
        public string Status_code { get; set; }
        public string status_desc { get; set; }       
        public string period_from { get; set; }       
        public string period_to { get; set; }
        public string untillactive { get; set; }  
        public string account1_code { get; set; }
        public string account2_code { get; set; }
        public string account1_desc { get; set; }
        public string account2_desc { get; set; }
        public string Recon_type { get; set; }
        public string dbsource { get; set; }
        public string select { get; set; }    
        public int result { get; set; }
        public string msg { get; set; }

        public string ip_address { get; set; }
        public string user_code { get; set; }
        public string recon_tallied { get; set; }
        public Recontype_model()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST 
            ip_address = Dns.GetHostByName(hostName).AddressList[0].ToString();
        }
    }
}
