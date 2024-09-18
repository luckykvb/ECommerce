using AutoMapper;
using ECommerce.Api.Products.DB;
using ECommerce.Api.Products.Profiles;
using ECommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace ECommerce.Api.Products.Tests
{
    public class ProductServiceTests
    {
        [Fact]
        public async void GetProductsReturnsAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts)).Options;
            var dbcontext = new ProductsDbContext(options);
            CreateProducts(dbcontext);

            var productprofiles = new ProductsProfile();
            var configuration = new MapperConfiguration(cfg=>cfg.AddProfile(productprofiles));
            var mapper = new Mapper(configuration);
            var productprovider = new ProductsProvider(dbcontext,mapper);
           var product = await productprovider.GetProductsAsync();
            Assert.True(product.IsSuccess);
            Assert.True(product.products.Any());
            Assert.Null(product.ErrorMessage);
        }

        [Fact]
        public async void GetProductsReturnsProductsUsingValidID()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsProductsUsingValidID)).Options;
            var dbcontext = new ProductsDbContext(options);
            CreateProducts(dbcontext);

            var productprofiles = new ProductsProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productprofiles));
            var mapper = new Mapper(configuration);
            var productprovider = new ProductsProvider(dbcontext, mapper);
            var product = await productprovider.GetProductsAsync(1);
            Assert.True(product.IsSuccess);
            Assert.NotNull(product.product);
            Assert.True(product.product.Id == 1);
            Assert.Null(product.ErrorMessage);
        }

        [Fact]
        public async void GetProductsReturnsProductsUsingInValidID()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsProductsUsingInValidID)).Options;
            var dbcontext = new ProductsDbContext(options);
            CreateProducts(dbcontext);

            var productprofiles = new ProductsProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productprofiles));
            var mapper = new Mapper(configuration);
            var productprovider = new ProductsProvider(dbcontext, mapper);
            var product = await productprovider.GetProductsAsync(-1);
            Assert.False(product.IsSuccess);
            Assert.Null(product.product);
            Assert.NotNull(product.ErrorMessage);
        }

        private void CreateProducts(ProductsDbContext productsDbContext)
        {
            for (var i = 1; i <= 10; i++)
            {
                var product = productsDbContext.Products.Add(
                    new Product
                    {
                        Id = i,
                        Name = Guid.NewGuid().ToString(),
                        Inventory = i + 10,
                        Price = (decimal)(i * 3.14)
                    });
            }
            productsDbContext.SaveChanges();
        }
    }
}