using AutoMapper;
using ECommerce.Api.Customers.DB;
using ECommerce.Api.Customers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Customers.Providers
{
    public class CustomerProvider : ICustomerProvider
    {
        private readonly CustomerDbContext dbContext;
        private readonly IMapper mapper;

        public CustomerProvider(CustomerDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            SetCustomerData();
        }

        private void SetCustomerData()
        {
            dbContext.Customers.Add(new DB.Customers { Id = 1, Name = "vb", Address ="h-no:1-88,near hitchcity, hyderabad,50038" });
            dbContext.Customers.Add(new DB.Customers { Id = 2, Name = "sushmitha",
                Address = "h-no:1-88,near hitchcity, hyderabad,50038" });
            dbContext.Customers.Add(new DB.Customers { Id = 3, Name = "keerthana", Address = "h-no:1-88,near hitchcity, Banglore,433038" });
            dbContext.Customers.Add(new DB.Customers { Id = 4, Name = "jaga", Address = "h-no:1-88,near hitchcity, palasas,523238" });
            dbContext.SaveChanges();
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Customers> customers, string ErrorMsg)> GetCustomerData()
        {
			try
			{
				var custmers = await dbContext.Customers.ToListAsync();
                if (custmers != null && custmers.Any())
                {
                    var result = mapper.Map<IEnumerable<Models.Customers>>(custmers);
                    return (true, result,null);
                }
                return (false, null, null);
			}
			catch (Exception ex)
			{

				throw;
			}
        }
        public async Task<(bool IsSuccess, Models.Customers customer, string ErrorMsg)> GetCustomerData(int customerId)
        {
            try
            {
                var customer = await dbContext.Customers.FirstOrDefaultAsync(x => x.Id == customerId);
                if (customer != null)
                {
                    var result  = mapper.Map<Models.Customers>(customer);
                    return (true,result,null);
                }
                return(false, null, "No Record Found");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
