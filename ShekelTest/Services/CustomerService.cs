using ShekelTest.DataAccess;
using ShekelTest.Models;

namespace ShekelTest.Services
{
    public class CustomerService : ICustomersService
    {
        public AddCustomer AddCustomer(AddCustomer customerItem)
        {
            DBRepository.Instance.AddCustomer(customerItem);
            return customerItem;
        }

        public List<ListCustomers> GetCustomers()
        {
            return DBRepository.Instance.GetCustomers();
        }
    }
}
