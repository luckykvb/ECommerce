namespace ECommerce.Api.Products.Profiles
{
    public class ProductsProfile : AutoMapper.Profile
    {
        public ProductsProfile() { 
         CreateMap<DB.Product,Models.Product>();
        }

    }
}
