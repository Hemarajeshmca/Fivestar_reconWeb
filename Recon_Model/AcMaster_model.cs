using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Recon_Model
{
    public class AcMaster_model
    {
        public int Account_id { get; set; }
       [Required(ErrorMessage = "Account No Should Not Be Blank")]
        public string AccountNo { get; set; }
         [Required(ErrorMessage = "Account Name Should Not Be Blank")]
        public string AccountName { get; set; }
        public Int64 Category_id { get; set; }
        public string Category_name { get; set; }
        public Int64 Respon_id { get; set; }
        public string Respon_name { get; set; }
        public string Recontype { get; set; }
        public string Accounttype { get; set; }
        public string Deduperule_id { get; set; }
        public string Dedup_rule { get; set; }
        public string Status { get; set; }
        public string status_desc { get; set; }
        public string Name { get; set; }       
        public string dbsource { get; set; }
        public string Dedupvalidateion { get; set; }
        public int result { get; set; }
        public string msg { get; set; }
        public string withinflag { get; set; }
       
        public string action { get; set; }
        public string ip_address { get; set; }
        public string user_code { get; set; }
        public AcMaster_model()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST 
            ip_address = Dns.GetHostByName(hostName).AddressList[0].ToString();
        }
    }
}
