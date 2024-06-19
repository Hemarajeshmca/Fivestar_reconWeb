using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Recon_Model
{
   public  class Report_model
    {
        public string table_name { get; set; }
        public string Report_condition { get; set; }

        public int koid { get; set; }
        public string kodate { get; set; }
        public string reconname { get; set; }
        public string rulename { get; set; }
        public string mappingdesctype { get; set; }
        public string matchoff_type { get; set; }
        public string Knockoffby { get; set; }
        public string Knockoffamount { get; set; }
        public string review { get; set; }
        public string user_code { get; set; }

        public string period_from { get; set; }
        public string period_to { get; set; }
        public string file_type { get; set; }
        public string filetype_desc { get; set; }
        public string import_date { get; set; }
        public int file_gid { get; set; }
        public string file_name { get; set; }
        public int report_gid{ get; set; }
        public string active_status { get; set; }

        public string particular { get; set; }
        public string amount { get; set; }
        public string accmode { get; set; }
        public string bal_amount { get; set; }
        public string tran_date { get; set; }
        public int brs_gid { get; set; }
        public int recon_gid { get; set; }

        public string field_type { get; set; }
        public int field_width { get; set; }

        public string errline_desc { get; set; }
        public int errline_gid { get; set; }
        public int errline_no { get; set; }

        public int result { get; set; }
        public string msg { get; set; }

        public string ip_address { get; set; }
        public int tran_gid { get; set; }
        public string Accountno { get; set; }

        public int Amount { get; set; }

        public bool in_outfile_flag { get; set; }
        public bool resultset_flag { get; set; }

        public DateTime transdate { get; set; }

        public string TranDescription { get; set; }

        public int MappedAmount { get; set; }

        public string report_code { get; set; }

        public string Select { get; set; }

        public string recon_tallied { get; set; }

        public int Sno { get; set; }

        public string Foldersname { get; set; }
        public string subFoldersname { get; set; }
        public string ExcelFiles { get; set; }

        public string glcode { get; set; }

        public Report_model()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST 
            ip_address = Dns.GetHostByName(hostName).AddressList[0].ToString();
        }

        public class report_type
        {
            public string type_code { get; set; }
            public string type_name { get; set; }
        }
    }
}
