using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Recon_Model
{
    public class Formula_model
    {
        public int sno { get; set; }
        public int filetemplatefield_gid { get; set; }
        public int filetemplatefieldformula_gid { get; set; }
        public string Sourcefieldname { get; set; }
        public string ExtractionCriteria { get; set; }
        public string ExtractionCriteria_desc { get; set; }
        public int extraction_filter { get; set; }
        public int formula_order { get; set; }
        public string lookup_flag { get; set; }
        public string lookup_master { get; set; }
        public string lookup_field { get; set; }
        public string lookup_extraction_field { get; set; }
        public string source_csv_column { get; set; }
        public int source_txt_start { get; set; }
        public int source_txt_end { get; set; }
        public string lookup_table_code { get; set; }
        public string lookup_extraction_criteria { get; set; }
        public int lookup_extraction_filter { get; set; }
        public string prefix_value { get; set; }
        public string suffix_value { get; set; }
        public string active_status { get; set; }
        public string active_status_desc { get; set; }       
        public string ip_address { get; set; }
        public string user_code { get; set; }
        public int TxtLenght { get; set; }

       public int master_gid { get; set; }
       public string master_syscode { get; set; }
       public string master_code { get; set; }
       public string master_short_code { get; set; }
       public string master_name { get; set; }
       public string parent_master_syscode { get; set; }

        public Formula_model()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST 
            ip_address = Dns.GetHostByName(hostName).AddressList[0].ToString();
        }
    }
}
