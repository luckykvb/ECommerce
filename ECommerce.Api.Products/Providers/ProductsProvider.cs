using AutoMapper;
using ECommerce.Api.Products.DB;
using ECommerce.Api.Products.Interfaces;
using ECommerce.Api.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections;

namespace ECommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvides
    {
        private readonly ProductsDbContext dbcontext;
       // private readonly Logger<ProductsProvider> logger;
        private readonly IMapper mapper;

        public ProductsProvider(ProductsDbContext dbContext,
            IMapper mapper )
        {
            this.dbcontext = dbContext;
            //this.logger = logger;
            this.mapper = mapper;
            SetData();
        }

        private void SetData()
        {
            if (!dbcontext.Products.Any())
            {
                dbcontext.Products.Add(new DB.Product() { Id = 1, Name = "Keyboard", Price = 100, Inventory = 200 });
                dbcontext.Products.Add(new DB.Product() { Id = 2, Name = "headset", Price = 120, Inventory = 300 });
                dbcontext.Products.Add(new DB.Product() { Id = 3, Name = "cpu", Price = 130, Inventory = 400 });
                dbcontext.Products.Add(new DB.Product() { Id = 4, Name = "mouse", Price = 50, Inventory = 500 });
                dbcontext.SaveChanges();
            }
        }

        public ProductsDbContext DbContext { get; }

        public async Task<(bool IsSuccess, IEnumerable<Models.Product> products, 
            string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await dbcontext.Products.ToListAsync();
                if(products !=null && products.Any())
                {
                    var result = mapper.Map<IEnumerable<Models.Product>>(products);
                    return (true, result,null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                //logger?.LogError(ex.Message.ToString());
                throw;
            }
        }

        public async Task<(bool IsSuccess, Models.Product product, string ErrorMessage)> GetProductsAsync(int ID)
        {
            try
            {
                var product = await dbcontext.Products.FirstOrDefaultAsync(x => x.Id == ID);
                if (product != null)
                {
                    var result = mapper.Map<Models.Product>(product);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                //logger?.LogError(ex.Message.ToString());
                throw;
            }
        }
    }
}
