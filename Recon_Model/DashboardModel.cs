using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recon_Model
{
    public class DashboardModel
    {
        public string periodfrom { get; set; }
        public string periodto { get; set; }
        public string ip_address { get; set; }
        public string user_code { get; set; }

        public int recon_count { get; set; }
        public int acc_count { get; set; }
        public int tran_count { get; set; }
        public int ko_count { get; set; }
        public int excp_count { get; set; }
        public int system_ko_count { get; set; }
        public int manual_ko_count { get; set; }
        public string KO_manualcount { get; set; }
        public int Ko_partialcount { get; set; }
        public int openingexcp_count { get; set; }
        public decimal ko_system_count { get; set; }
        public decimal ko_manual_count { get; set; }

        public string recon_gid { get; set; }
        public string ko_month { get; set; }
        public string aging_desc { get; set; }
        public string excpagaing_count { get; set; }

        public int ReconName_id { get; set; }
        public string ReconName { get; set; }
        public string active_status { get; set; }
        public string recontype { get; set; }
       
        




    }
}
