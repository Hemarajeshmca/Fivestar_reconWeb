using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Recon_Model
{
   public class FileConTemp_model
    {
       public int slno { get; set; }
       public string Sourcefieldname { get; set; }
       public string DBfieldname { get; set; }
       public string SourceColumnnum { get; set; }
       public string SourceTextformat { get; set; }
       public string length { get; set; }
       public string Select { get; set; }
       public string code { get; set; }
       public string Name { get; set; }
       public string StartPosition { get; set; }
       public string EndPosition { get; set; }

       public Int64 Template_gid { get; set; }
       public string Template_name { get; set; }
       public string Template_type { get; set; }
       public string Template_type_desc { get; set; }
       public string FileType { get; set; }
       public string FileType_desc { get; set; }
       public string Hflag { get; set; }
       public int Csv_Columns { get; set; }
       public string Csv_seperator { get; set; }
       public string Acflag { get; set; }
       public string Transaction_Amount_type { get; set; }
       public string Transaction_Amount_type_desc { get; set; }
       public string Active_status { get; set; }
       public string Blflag { get; set; }
       public string Balance_Amount_type { get; set; }
       public string Balance_Amount_type_desc { get; set; }
       public string Tran_date_format { get; set; }
       public string Action { get; set; }

       public Int64 Header_id { get; set; }

       public string field_name { get; set; }
       public Int32 Csv_Column_No { get; set; }
       public Int32 Txt_Start_Position { get; set; }
       public Int32 Txt_End_Position { get; set; }
       public string Txt_Length { get; set; }
       public string field_alias_name { get; set; }
       public string Mandatory_Field { get; set; }
       public string Mandatory_Fieldvalue { get; set; }
       public string Field_Active_Status { get; set; }
       public string Field_Active_desc { get; set; }
       public string sno { get; set; }
       public string fieldmappingtype { get; set; }
       public Int64 FileTemplatefield_gid { get; set; }

       public string ip_address { get; set; }
       public string user_code { get; set; }

       public string dropdowntype { get; set; }
       public string param_1 { get; set; }

       public string applyfiltercode { get; set; }
       public string applyfilterdesc { get; set; }

       public string jobtypecode { get; set; }
       public string jobtypedesc { get; set; }

       public string master_name { get; set; }

       public string master_code { get; set; }

       public string applyfilter_code { get; set; }
      public string applyfilter_desc { get; set; }
      public int applyrule_basesele_gid { get; set; }
      public string Mandatory_Fielddummy { get; set; }

       public FileConTemp_model()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST 
            ip_address = Dns.GetHostByName(hostName).AddressList[0].ToString();
        }
    }
}
