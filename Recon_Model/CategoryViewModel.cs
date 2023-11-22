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
    public class CategoryViewModel
    {       
        public int category_id { get; set; }
        [Required(ErrorMessage = "Category Name Should Not Be Blank")]
        public string Category_name { get; set; }
        [Required(ErrorMessage = "Status Should not be blank")]
        public string Status { get; set; }
        public string Status_desc { get; set; }
        public string dbsource { get; set; }
        public int result { get; set; }
        public string msg { get; set; }
        public int tranbrkptype_gid { get; set; }
        public string tranbrkptype_name { get; set; }
        public string user_code { get; set; }
        public int remark_gid { get; set; }
        public string remark_desc { get; set; }

        public int tranrecon_gid { get; set; }
        public string remark1 { get; set; }
        public string remark2 { get; set; }

        public string ip_address { get; set; }
        public int tran_gid { get; set; }

        public CategoryViewModel()
        {

            string hostName = Dns.GetHostName(); // Retrive the Name of HOST 
            ip_address = Dns.GetHostByName(hostName).AddressList[0].ToString();
            
        }
    }
}
