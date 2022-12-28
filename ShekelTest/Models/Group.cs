namespace ShekelTest.Models
{
    public class Group
    {
        public int GroupCode { get; set; }
        public string GroupName { get; set; }
        public List<BaseCustomer> Customers { get; set; }       
    }
}
