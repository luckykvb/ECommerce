using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using System.Text.Json;

namespace ECommerce.Api.Search.Services
{
    public class ProductService : IProductsService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public ProductService(IHttpClientFactory httpClientFactory) {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<(bool IsSuccess, IEnumerable<Product> products, string ErrorMessage)> GetProductAsync()
        {
            try
            {
                var client = httpClientFactory.CreateClient("ProductService");
                var response = await client.GetAsync($"api/products");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<IEnumerable<Product>>(content,option);
                    return (true,result,null);
                }
                return (false, null, response.ReasonPhrase);
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message.ToString());
            }
        }
    }
}
