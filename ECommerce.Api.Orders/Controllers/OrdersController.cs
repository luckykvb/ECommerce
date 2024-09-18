using ECommerce.Api.Orders.DB;
using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Orders.Controllers
{
    [ApiController]
    [Route("api/Orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderProvider orderProvider;

        public OrdersController(IOrderProvider orderProvider)
        {
            this.orderProvider = orderProvider;
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetOrdersAsync(int Id)
        {
            var orders = await orderProvider.GetOrderAsync(Id);
            if (orders.IsSuccess)
            {
                return Ok(orders.Orders);
            }
            return NotFound();
            {

            }
        }
    }
}
