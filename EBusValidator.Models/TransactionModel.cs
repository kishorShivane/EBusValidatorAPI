using System;

namespace EBusValidator.Models
{
    public class TransactionModel
    {
        public int ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime TransactionDate { get; set; }
        public string CardESN { get; set; }
        public int BusNumber { get; set; }
        public string GPSLatitude { get; set; }
        public string GPSLongitude { get; set; }
        public int Action { get; set; }
    }
}
