using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recon_Model
{
    public class Partialgrid
    {
        public int tran_gid { get; set; }

        public String Accountno { get; set; }

        public int Amount { get; set; }

        public DateTime transdate { get; set; }

        public int MappedAmount { get; set; }

        public string TranDescription { get; set; }
    }
}
