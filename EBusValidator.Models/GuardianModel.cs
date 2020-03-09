using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBusValidator.Models
{
    public class GuardianModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string RelationShip { get; set; }
        public string CellPhone { get; set; }
        public string TelePhone { get; set; }
        public string EMail { get; set; }
    }
}
