using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Recon_Model
{
   public  class AccountBlncRpt_model
    {
       public string Tran_date { get; set; }
       public Int16 SlNo { get; set; }
       public string AccNo { get; set; }
       public string AccName { get; set; }
       public string Tran_date_rpt { get; set; }
       public string Tran_Balance_Amount { get; set; }
       public string Arrived_amount { get; set; }
       public string status { get; set; }
       public int recon_gid { get; set; }

       public string account1_desc { get; set; }
       public string account1_code { get; set; }

       public string status_flag { get; set; }
       public string status_desc { get; set; }

       public string ip_address { get; set; }
       public string user_code { get; set; }
       public AccountBlncRpt_model()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST 
            ip_address = Dns.GetHostByName(hostName).AddressList[0].ToString();
        }
    }
}
