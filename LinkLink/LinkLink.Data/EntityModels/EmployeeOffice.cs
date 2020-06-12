namespace LinkLink.Data.EntityModels
{
    public class EmployeeOffice
    {
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public string OfficeId { get; set; }
        public Office Office { get; set; }
    }
}
