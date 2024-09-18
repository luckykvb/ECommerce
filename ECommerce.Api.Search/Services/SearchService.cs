
using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrderService orderService;
        private readonly IProductsService products;
        private readonly ICustomerService customerService;

        public SearchService(IOrderService orderService, IProductsService products,ICustomerService customerService)
        {
            this.orderService = orderService;
            this.products = products;
            this.customerService = customerService;
        }
        public async Task<(bool IsSuccess, dynamic SearchItems)> SearchAsynce(int customerId)
        {
            var customerData = await customerService.GetCustomerAsync(customerId);
            var orderResult = await orderService.GetOrdersAsync(customerId);
            var ProductsResult = await products.GetProductAsync();
            

            if (orderResult.IsSuccess)
            {
                foreach (var order in orderResult.Orders)
                {
                    foreach (var item in order.items)
                    {
                        item.ProductName = ProductsResult.IsSuccess ? 
                            ProductsResult.products.FirstOrDefault(x => x.Id == item.Id)?.Name :
                            "Product Name is Not Available";   
                    }
                }
                var result = new
                {
                    customer = customerData.IsSuccess ? customerData.customers : 
                    new { Name ="Customer information is Not Available" },
                    Order = orderResult.Orders
                };
                return(true, result);
            }
            return(false, null);
        }
    }
}
