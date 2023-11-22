using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Recon_Model
{
  public class user_model
    {       
        public int user_gid { get; set; }
        public int sno { get; set; }
       [Required(ErrorMessage = "user code Should Not Be Blank")]
        public string user_code { get; set; }
       [Required(ErrorMessage = "user name Should Not Be Blank")]
        public string user_name { get; set; }
       [Required(ErrorMessage = "user address Should Not Be Blank")]
        public string user_address { get; set; }
        public string city_name { get; set; }
       [Required(ErrorMessage = "pin code Should Not Be Blank")]
        public string pin_code { get; set; }
       [Required(ErrorMessage = "State name Should Not Be Blank")]
        public string state_name { get; set; }
       [Required(ErrorMessage = "Contact No Should Not Be Blank")]
        public string user_contact_no { get; set; }
       [Required(ErrorMessage = "Email Should Not Be Blank")]
        public string user_emailid { get; set; }
        public string security_question { get; set; }
        public string security_answer { get; set; }
       [Range(1, 50)]
       [Required(ErrorMessage = "user group Should Not Be Blank")]
        public int usergroup_gid { get; set; }      
        public string usergroup_code { get; set; }
       [Required(ErrorMessage = "user type Should Not Be Blank")]
        public string user_type { get; set; }
        public string user_type_desc { get; set; }
        public string user_status { get; set; }
        public string active_status_desc { get; set; }

        public string oldpassworrd { get; set; }
        public string passwordexpdate { get; set; }
        public string newpassword { get; set; }
        public string passwordreset { get; set; }
        public string treearray { get; set; }

        public string FromEmailId { get; set; }
        public string Password { get; set; }
        public int SMTPPort { get; set; }
        public string Host { get; set; }

        public string dbsource { get; set; }
        public int result { get; set; }
        public string msg { get; set; }
        public string ReconName_id { get; set; }
        public string recon_gid { get; set; }

        public string ip_address { get; set; }
        public user_model()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST 
            ip_address = Dns.GetHostByName(hostName).AddressList[0].ToString();
        }

        public partial class menu
        {
            public int menu_gid { get; set; }
            public string menu_name { get; set; }
            public string menu_url { get; set; }
            public int parent_menu_gid { get; set; }
            public int menu_order { get; set; }
            public bool rights_flag { get; set; }
        }
    }
}
