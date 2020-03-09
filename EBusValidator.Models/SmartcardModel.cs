using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBusValidator.Models
{
    public class SmartcardModel
    {
        public SmartcardModel()
        {
            Guardians = new List<GuardianModel>();
        }
        public int ID { get; set; }
        public string SmartcardNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string CardType { get; set; }
        public string CellPhone { get; set; }
        public string EMail { get; set; }
        public string Number { get; set; }
        public List<GuardianModel> Guardians { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public bool Status { get; set; }
    }
}
