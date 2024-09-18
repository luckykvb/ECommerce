using ECommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Customers.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerProvider customerProvider;

        public CustomerController(ICustomerProvider customerProvider)
        {
            this.customerProvider = customerProvider;
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomerInfo()
        {
            var custoinfo = await customerProvider.GetCustomerData();
            if (custoinfo.IsSuccess)
            {
                return Ok(custoinfo.customers);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomers(int id)
        {
            var customer = await customerProvider.GetCustomerData(id);
            if (customer.IsSuccess)
            {
                return Ok(customer.customer);
            }
            return NotFound();
        }
    }
}
