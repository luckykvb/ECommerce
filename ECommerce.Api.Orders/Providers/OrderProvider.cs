using AutoMapper;
using ECommerce.Api.Orders.DB;
using ECommerce.Api.Orders.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Orders.Providers
{
    public class OrderProvider : IOrderProvider
    {
        private readonly OrderDbContext dbContext;
        private readonly IMapper mapper;

        public OrderProvider(OrderDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            SetOrderData();
        }

        private void SetOrderData()
        {
            if (!dbContext.Order.Any())
            {
                dbContext.Order.Add(new DB.Order()
                {
                    Id = 1,
                    CustomerID = 1,
                    OrderDate = DateTime.Now,
                    Total = 123,
                    Items = new List<DB.OrderItem>
                             {
                                 new DB.OrderItem
                                 {
                                     Id = 1,
                                     OrderId = 1,
                                     ProductId = 2,
                                     Quantity = 123,
                                     UnitPrice = 1099
                                 }
                             }
                });
                dbContext.Order.Add(new DB.Order()
                {
                    Id = 2,
                    CustomerID = 2,
                    OrderDate = DateTime.Now,
                    Total = 124,
                    Items = new List<DB.OrderItem>
                             {
                                 new DB.OrderItem
                                 {
                                     Id = 2,
                                     OrderId = 2,
                                     ProductId = 2,
                                     Quantity = 123,
                                     UnitPrice = 1099
                                 }
                             }
                });
                dbContext.Order.Add(new DB.Order()
                {
                    Id = 3,
                    CustomerID = 3,
                    OrderDate = DateTime.Now,
                    Total = 194,
                    Items = new List<DB.OrderItem>
                             {
                                 new DB.OrderItem
                                 {
                                     Id = 3,
                                     OrderId = 3,
                                     ProductId = 2,
                                     Quantity = 123,
                                     UnitPrice = 1099
                                 }
                             }

                });
                dbContext.Order.Add(new DB.Order()
                {
                    Id = 4,
                    CustomerID = 4,
                    OrderDate = DateTime.Now,
                    Total = 144,
                   Items = new List<DB.OrderItem>
                             {
                                 new DB.OrderItem
                                 {
                                     Id = 4,
                                     OrderId = 4,
                                     ProductId = 2,
                                     Quantity = 123,
                                     UnitPrice = 1099
                                 }
                             }
                });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrderAsync(int orderId)
        {
            try
            {
                var Orders = await dbContext.Order.Where(x => x.Id == orderId).ToListAsync();
                if (Orders != null)
                {
                    var result = mapper.Map<IEnumerable<Models.Order>>(Orders);
                    return (true, result, null);
                }
                return (false, null, "No Orders Found");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
