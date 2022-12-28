namespace ShekelTest.Models
{
    public class Customer : BaseCustomer
    {
        public int GroupCode { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int FactoryCode { get; set; }
    }
}
