using ShekelTest.DataAccess;
using ShekelTest.Models;

namespace ShekelTest.Services
{
    public class CustomerService : ICustomersService
    {
        public Customer AddCustomer(Customer customerItem)
        {
            DBRepository.Instance.AddCustomer(customerItem);
            return customerItem;
        }

        public List<Group> GetGroups()
        {
            return DBRepository.Instance.GetGroups();
        }
    }
}
