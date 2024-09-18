using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using System.Text.Json;

namespace ECommerce.Api.Search.Services
{
    public class OrderService : IOrderService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public OrderService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrdersAsync(int CustomerId)
        {
            try
            {
                var client = httpClientFactory.CreateClient("OrderService");
                var response = await client.GetAsync($"api/Orders/{CustomerId}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true }; 
                    var result = JsonSerializer.Deserialize<IEnumerable<Order>>(content,option);
                    return (true,result,null);
                }
                return (false,null,response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                return (false,null,ex.Message.ToString());
            }
        }
    }
}
