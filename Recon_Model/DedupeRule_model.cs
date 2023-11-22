using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Recon_Model
{
    public class DedupeRule_model
    {
        public int slno { get; set; }
        public int sno { get; set; }
        public int slnolist { get; set; }
         [Required(ErrorMessage = "Field Should Not Be Blank")]
        public string field_name { get; set; }
        public string field_alias_name { get; set; }
        public string ConditionCriteria { get; set; }
        public string Select { get; set; }
         [Required(ErrorMessage = "Extraction Criteria Should Not Be Blank")]
        public string ExtractionCriteria { get; set; }
         [Required(ErrorMessage = "Comparison Criteria Should Not Be Blank")]
        public string ComparisonCriteria { get; set; }
         [Required(ErrorMessage = "Target Field Should Not Be Blank")]
        public string TargetField { get; set; }
        public string TargetField_alias { get; set; }
        public string field_type { get; set; }
        public string Name { get; set; }
        public string Deduperule { get; set; }
        public string status { get; set; }
        public string status_desc { get; set; }
        public string period_from { get; set; }
        public string period_To { get; set; }
        public string Rule_name { get; set; }
        public string dbsource { get; set; }
        public string group_flag { get; set; }
        public string untillactive { get; set; }
        public bool target_ident_flag { get; set; }
        public string target_ident_value { get; set; }
        public bool source_ident_flag { get; set; }
        public string source_ident_value { get; set; }
        public string user_code { get; set; }
        public int filter_flag { get; set; }
        public string filter_desc { get; set; }
        public string function_name { get; set; }
        public int selected_status { get; set; }
        public int comparision_filter { get; set; }
        public int extraction_filter { get; set; }

        public int result { get; set; }
        public string msg { get; set; }
        public string field_name_txt { get; set; }

        public string insertdate { get; set; }
        public string updatedate { get; set; }

        public string ip_address { get; set; }
        public DedupeRule_model()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST 
            ip_address = Dns.GetHostByName(hostName).AddressList[0].ToString();
        }
    }
}
