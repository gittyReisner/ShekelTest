using ShekelTest.DataAccess;
using ShekelTest.Models;

namespace ShekelTest.Services
{
    public class CustomerService : ICustomersService
    {
        private List<Customer> _customerItems;
        public CustomerService()
        {
            _customerItems = new List<Customer>();
        }
        public Customer AddCustomer(Customer customerItem)
        {
            _customerItems.Add(customerItem);
            return customerItem;
        }

        public List<Customer> GetCustomers()
        {
            DBRepository.Instance.get();
            return null;
        }
    }
}
