using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBusValidator.Models
{
    public class SearchParams
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Smartcard { get; set; }
    }
}
