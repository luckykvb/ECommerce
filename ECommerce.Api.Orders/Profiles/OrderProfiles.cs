using AutoMapper;

namespace ECommerce.Api.Orders.Profiles
{
    public class OrderProfiles : AutoMapper.Profile
    {
        public OrderProfiles()
        {
            CreateMap<DB.Order,Models.Order>();
            CreateMap<DB.OrderItem,Models.OrderItem>();
        }

    }
}
