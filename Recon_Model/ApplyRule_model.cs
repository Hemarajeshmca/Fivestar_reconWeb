using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Recon_Model
{
    public class ApplyRule_model
    {
        public int listslno { get; set;}
        public string acc_no { get; set; }
        public string acc_desc { get; set; }
        public string acct_no { get; set; }
        public string acct_desc { get; set; }
        public int slno { get; set; }
        public int sno { get; set; }
        public string noofbase { get; set; }
        public int slnoadd { get; set; }
        public string Identitycriteria { get; set; }
        public int slnobase { get; set; }
        public int slnobaseFilter { get; set; }
        public int slnorule { get; set; }
        public string ReconName { get; set; }
        public int ReconName_id { get; set; }
        public string supporting_file { get; set; }
        public int supporting_file_id { get; set; }
        public string matchoffSys { get; set; }
        public string matchoffMan { get; set; }
        public string RuleName { get; set; }
        public int Rulegid { get; set; }
        public string reversl_flag { get; set; }
        public string group_flag { get; set; }
        public string period_from { get; set; }
        public string period_to { get; set; }       
        public string untillactive { get; set; } 
        public string Ruleorder { get; set; }
         //[Required(ErrorMessage = "Account Mode Should Not Be Blank")]
        public string Accountmode { get; set; }
        public string Accountmode_alias_name { get; set; }
        public string account1 { get; set; }
        public string account2 { get; set; }
        public string Select { get; set; }
        //   [Required(ErrorMessage = "Extraction Criteria Should Not Be Blank")]
        public string ExtractionCriteria { get; set; }
           //[Required(ErrorMessage = "Base Criteria Should Not Be Blank")]
           public string BaseCriteria { get; set; }
        public string ExtractionCriteria_alias_name { get; set; }
        public string groupCriteria { get; set; }
         //[Required(ErrorMessage = "Comparison Criteria Should Not Be Blank")]
        public string ComparisonCriteria { get; set; }
        
        public string ComparisonCriteria_alias_name { get; set; }
         //[Required(ErrorMessage = "Target Field Should Not Be Blank")]
        public string TargetField { get; set; }
         //[Required(ErrorMessage = "Field Should Not Be Blank")]
        public string field_name { get; set; }
        public string TargetField_alias { get; set; }
        public string field_alias_name { get; set; }
        //[Required(ErrorMessage = "Order Should Not Be Blank")]
        public string Order { get; set; }
       public string comparecrtieria { get; set; }
        public string status { get; set; }
        public string status_desc { get; set; }
        public string Recontype { get; set; }
        public string AccountnoRecName { get; set; }
        public string groupflag { get; set; }
        public string group_method { get; set; }
        public string group_many { get; set; }
        public string dbsource { get; set; }
        public int result { get; set; }
        public string msg { get; set; }
        public int  applyruledtl_gid { get; set; }
        public string user_code { get; set; }
        public bool add { get; set; }
        public int recon_gid { get; set; }
        public int filter_flag { get; set; }
        public string filter_desc { get; set; }
        public int comparision_filter { get; set; }
        public int extraction_filter { get; set; }
        public string ip_address { get; set; }
        public string in_action { get; set; }

        public string identy_value { get; set; }
        public string filter_appliedon { get; set; }
        public string filter_appliedon_desc { get; set; }
        public int DkReconName_id { get; set; }
        public string target_acc_desc { get; set; }
        public int applyrule_basefilter_gid { get; set; }
        public int Additional_gid { get; set; }
        public int applyrule_basesele_gid {get; set; }
        public string target_grpfield { get; set; }
        public int targetField_gid { get; set; }
        public int target_grpfield_gid { get; set; }
        public string target_grpfield_alias { get; set; }

        public ApplyRule_model()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST 
            ip_address = Dns.GetHostByName(hostName).AddressList[0].ToString();
        }
    }
    
}
