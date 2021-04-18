using System;

namespace EBusValidator.Models
{
    public class SearchParams
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Smartcard { get; set; }
        public int BusNumber { get; set; }
        public string DriverNumber { get; set; }
    }
}
