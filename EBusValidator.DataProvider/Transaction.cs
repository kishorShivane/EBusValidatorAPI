//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EBusValidator.DataProvider
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transaction
    {
        public int Id { get; set; }
        public System.DateTime Created { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public string DeviceSerial { get; set; }
        public string CardEsn { get; set; }
        public int BusNumber { get; set; }
        public double GpsLatitude { get; set; }
        public double GpsLongtitude { get; set; }
        public int Action { get; set; }
        public int StoredValue { get; set; }
        public int Trips { get; set; }
    }
}
