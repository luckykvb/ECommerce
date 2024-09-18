namespace ECommerce.Api.Customers.Profiles
{
    public class CustomerProfile : AutoMapper.Profile
    {
        public CustomerProfile() {
            CreateMap<DB.Customers, Models.Customers>();
        }
    }
}
