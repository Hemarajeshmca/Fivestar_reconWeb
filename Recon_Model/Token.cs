using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recon_Model
{
    public class Token
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }

        ////      [JsonProperty("access_token")]
        //public string AccessToken { get; set; }
        ////    [JsonProperty("token_type")]
        //public string TokenType { get; set; }
        ////  [JsonProperty("expires_in")]
        //public int ExpiresIn { get; set; }
        ////[JsonProperty("refresh_token")]
        //public string RefreshToken { get; set; }
        ////[JsonProperty("error")]
        //public string Error { get; set; }
    }
}
