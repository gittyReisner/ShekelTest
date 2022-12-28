using ShekelTest.Models;

namespace ShekelTest.Services
{
    public interface ICustomersService
    {
        public List<Group> GetGroups();

        public Customer AddCustomer(Customer customerItem);

    }
}
