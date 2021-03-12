﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBusValidator.Models
{
    public class UsageHistoryModel
    {
        public string SurName { get; set; }
        public string FirstName { get; set; }
        public string Smartcard { get; set; }
        public int Action { get; set; }
        public string ActivityType { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int Bus { get; set; }
        public string Driver { get; set; }
    }
}
