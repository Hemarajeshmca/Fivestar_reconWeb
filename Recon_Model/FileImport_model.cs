using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Recon_Model
{
    public class FileImport_model
    {
        public Int64 Template_gid { get; set; }
        public Int64 filetemplatefield_gid { get; set; }
        public Int64 filetemplate_gid { get; set; }
        public string Template_name { get; set; }
        public string TemplateType_desc { get; set; }
        public string FileSeperator { get; set; }

        public string ExFieldName { get; set; }

        public int Template_id { get; set; }
        public int csv_column_no { get; set; }

        public int Txt_start_position { get; set; }
        public int Txt_end_position { get; set; }
        public int TxtLenght { get; set; }

        public string TranField { get; set; }
        public string TranField_Type { get; set; }

        public string FileName { get; set; }
        public string FileType { get; set; }
        public string Actionby { get; set; }

        public int LineNo { get; set; }
        public string file_gid { get; set; }
        public string acc_no { get; set; }
        public string tran_date { get; set; }
        public string value_date { get; set; }
        public string tran_desc { get; set; }
        public string amount { get; set; }
        public string mult { get; set; }
        public string acc_mode { get; set; }
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

        public string balance_flag { get; set; }
        public string bal_amount { get; set; }
        public string bal_amount_signed { get; set; }
        public string bal_amount_reverse_signed { get; set; }
        public string bal_amount_acc_mode { get; set; }
        public string debug_flag { get; set; }

        public string amount_signed { get; set; }
        public string amount_reverse_signed { get; set; }
        public string amount_debit { get; set; }
        public string amount_credit { get; set; }

        public Int64 recon_gid { get; set; }
        public string recon_name { get; set; }

        public Int64 tranbrkptype_gid { get; set; }
        public string tranbrkptype_name { get; set; }

        public Int64 reconacc_gid { get; set; }
        public string reconnacc_no { get; set; }

        public string ExcelFileName { get; set; }
        public string field_mapping_type { get; set; }
        public string field_mapping_type_desc { get; set; }

        public string ip_address { get; set; }
        public string user_code { get; set; }
        public string error_line { get; set; }
        public List<Formula_model> formula { get; set; }

        public string Hflag { get; set; }
        public int Csv_Columns { get; set; }
        public string Acflag { get; set; }
        public string Transaction_Amount_type { get; set; }
        public string Transaction_Amount_type_desc { get; set; }
        public string Blflag { get; set; }
        public string Balance_Amount_type { get; set; }
        public string Balance_Amount_type_desc { get; set; }
        public string active_status { get; set; }
        public string recontype { get; set; }
        public int ReconName_id { get; set; }
        public string ReconName { get; set; }
        public string filetype_desc { get; set; }
        public string Value { get; set; }
        public string AccType { get; set; }
        //public string filetype { get; set; }
        public string update_flag { get; set; }
        public string filetype_val { get; set; }


        public FileImport_model()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST 
            ip_address = Dns.GetHostByName(hostName).AddressList[0].ToString();
        }
       
    }
}
