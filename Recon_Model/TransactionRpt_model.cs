using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Recon_Model
{
    public class TransactionRpt_model
    {
        public int tran_gid { get; set; }

        public String Accountno { get; set; }

        public int Amounttr { get; set; }

        public DateTime transdate { get; set; }

        public string Trandate { get; set; }

        public string fromdate { get; set; }
        public string todate { get; set; }

        public int MappedAmount { get; set; }

        public string TranDescription { get; set; }
        public int report_giddrop { get; set; }
        public int reportid { get; set; }
        public int report_gid { get; set; }
       
        public string report_desc { get; set; }
        public string report_isremoved { get; set; }
        public int  reportparam_gid { get; set; }
        public string reportparam_code { get; set; }
        public string reportparam_value { get; set; }
        public string reportparam_isremoved { get; set; }

        public string account_no { get; set; }
        public string Period_from { get; set; }
        public string Period_To { get; set; }
        public string ReconName_id { get; set; }
        public int Recongid { get; set; }
        public string Recon_gid { get; set; }
        public string Amount { get; set; }
        public string Exception_amount { get; set; }

        public string Account_flag { get; set; }
        public string Account_desc { get; set; }

        public string status_flag { get; set; }
        public string status_desc { get; set; }

        public string Table_Name { get; set; }
        public string Report_condition { get; set; }

        public int Slno { get; set; }
        public string Account_Name { get; set; }
        public string PaymentRef { get; set; }
        public string Voucher_no { get; set; }
        public string Ref_no { get; set; }
        public string Account_mode { get; set; }
        public string Mult { get; set; }
        public string Amount_rpt { get; set; }
        public string Tran_desc { get; set; }
        public string Value_date { get; set; }
        public string Tran_date { get; set; }
        public string Acc_No { get; set; }
        public string File_Id { get; set; }
        public string Tran_id { get; set; }
        public string Recon_Name { get; set; }
        public string Cheque_No { get; set; }

        public string TranBkpType_Name { get; set; }
        public string TranBkpType_Gid { get; set; }
        public string Excp_Amount { get; set; }
        public string ref_col1 { get; set; }
        public string ref_col2 { get; set; }
        public string ref_col3 { get; set; }
        public string ref_col4 { get; set; }
        public string ref_col5 { get; set; }
        public string ref_col6 { get; set; }
        public string ref_col7 { get; set; }
        public string ref_col8 { get; set; }
        public string ref_col9 { get; set; }
        public string ref_col10 { get; set; }
        public string ref_col11 { get; set; }
        public string ref_col12 { get; set; }
        public string ref_col13 { get; set; }
        public string ref_col14 { get; set; }
        public string ref_col15 { get; set; }
        public string ref_col16 { get; set; }
        public string ref_col17 { get; set; }
        public string ref_col18 { get; set; }
        public string ref_col19 { get; set; }
        public string ref_col20 { get; set; }
        public string ref_col21 { get; set; }
        public string ref_col22 { get; set; }
        public string ref_col23 { get; set; }
        public string ref_col24 { get; set; }
        public string ref_col25 { get; set; }
        public string ref_col26 { get; set; }
        public string ref_col27 { get; set; }
        public string ref_col28 { get; set; }
        public string ref_col29 { get; set; }
        public string ref_col30 { get; set; }
        public string ref_col31 { get; set; }
        public string ref_col32 { get; set; }
        public string ko_date { get; set; }
        public string tranrecon_excp_amount { get; set; }
        public string mapped_amount { get; set; }
        public string tran_remark1 { get; set; }
        public string tran_remark2 { get; set; }
        public string tranrecon_ko_date { get; set; }
        public string import_date { get; set; }
        public string import_by { get; set; }

        public string ip_address { get; set; }
        public string user_code { get; set; }
        public int valuedrop { get; set; }
        public string matchofftype { get; set; }
        public string rulename { get; set; }
        public string drcount { get; set; }
        public string dramount { get; set; }
        public string crcount { get; set; }
        public string cramount { get; set; }
        public string TotalcountofNarration { get; set; }
        public string Totalsumofamount { get; set; }
        public string rowslabel { get; set; }
        public string color { get; set; }
        public string active_status { get; set; }
        public string ReconName { get; set; }
        public string recontype { get; set; }
        public string forecolor { get; set; }
        public string backcolor { get; set; }
        public int nullvalues { get; set; }
        public string recontallied { get; set; }
        public string recondaily_gid { get; set; }
        public int  no_of_recons { get; set; }

        public int Dropdownkoid { get; set; }
        public string Dropdownkovalue { get; set; }

        public TransactionRpt_model()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST 
            ip_address = Dns.GetHostByName(hostName).AddressList[0].ToString();
        }
    }
}
