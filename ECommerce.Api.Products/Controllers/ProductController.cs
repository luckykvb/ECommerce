using ECommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Products.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController :ControllerBase
    {
        private readonly IProductsProvides productsProvides;

        public ProductController(IProductsProvides productsProvides)
        {
            this.productsProvides = productsProvides;
        }
        [HttpGet]
        public async Task<IActionResult> GetProductAsync()
        {
            var produtsResponse = await productsProvides.GetProductsAsync();
            if(produtsResponse.IsSuccess)
            {
                return Ok(produtsResponse.products);
            }
            return NotFound();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var uniqueProduct = await productsProvides.GetProductsAsync(id);
            if (uniqueProduct.IsSuccess)
            {
                return Ok(uniqueProduct.product);
            }
            return NotFound();
        }
    }
}
