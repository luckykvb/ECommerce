using ECommerce.Api.Search.Models;

namespace ECommerce.Api.Search.Interfaces
{
    public interface ICustomerService
    {
       Task<(bool IsSuccess, dynamic customers, string ErrorMessage)> GetCustomerAsync(int Id);
    }
}
