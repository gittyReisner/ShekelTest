using ShekelTest.Models;

namespace ShekelTest.Services
{
    public interface ICustomersService
    {
        public List<Customer> GetCustomers();

        public Customer AddCustomer(Customer customerItem);

    }
}
