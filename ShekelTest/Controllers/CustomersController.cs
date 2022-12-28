using Microsoft.AspNetCore.Mvc;
using ShekelTest.Models;
using ShekelTest.Services;

namespace ShekelTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private ILogger _logger;
        private ICustomersService _service;


        public CustomersController(ILogger<CustomersController> logger, ICustomersService service)
        {
            _logger = logger;
            _service = service;

        }

        [HttpGet("/api/customers")]
        public ActionResult<List<ListCustomers>> GetCustomers()
        {
            return _service.GetCustomers();
        }

        [HttpPost("/api/customers")]
        public ActionResult<AddCustomer> AddCustomer(AddCustomer customerItem)
        {
            _service.AddCustomer(customerItem);
            return customerItem;
        }
    }
}
