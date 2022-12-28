using ShekelTest.Models;

namespace ShekelTest.Services
{
    public interface ICustomersService
    {
        public List<ListCustomers> GetCustomers();

        public AddCustomer AddCustomer(AddCustomer customerItem);

    }
}
