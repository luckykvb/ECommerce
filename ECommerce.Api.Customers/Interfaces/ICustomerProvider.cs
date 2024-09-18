
using ECommerce.Api.Customers.Models;

namespace ECommerce.Api.Customers.Interfaces
{
    public interface ICustomerProvider
    {
        Task<(bool IsSuccess,IEnumerable<Models.Customers> customers, string ErrorMsg)> GetCustomerData();
        Task<(bool IsSuccess, Models.Customers customer, string ErrorMsg)> GetCustomerData(int Id);
    }
}
