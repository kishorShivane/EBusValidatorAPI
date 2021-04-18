namespace EBusValidator.Models
{
    public class UsageSummaryModel
    {
        public string SurName { get; set; }
        public string FirstName { get; set; }
        public string AccountNumber { get; set; }
        public string Location { get; set; }
        public string Smartcard { get; set; }
        public int TotalTagIns { get; set; }
        public int Kilometers { get; set; }
    }
}
