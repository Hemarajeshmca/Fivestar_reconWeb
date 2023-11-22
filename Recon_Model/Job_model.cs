using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recon_Model
{
    public class Job_model
    {
        public Int64 Job_gid { get; set; }
        public string Job_type { get; set; }
        public string Job_type_desc { get; set; }
        public Int64 Job_ref_gid { get; set; }
        public string Job_name { get; set; }
        public string Job_initiated_by { get; set; }
        public string Job_ip_addr { get; set; }
        public string Job_status { get; set; }
        public string Job_status_desc { get; set; }
        public string Job_remark { get; set; }
        public string msg { get; set; }
        public int result { get; set; }
        public string Job_start_date { get; set; }
        public string Job_end_date { get; set; }
        public string File_path { get; set; }
    }
}
