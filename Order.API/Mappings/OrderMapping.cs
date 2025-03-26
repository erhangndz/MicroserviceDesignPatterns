using AutoMapper;
using Shared;

namespace Order.API.Mappings
{
    public class OrderMapping: Profile
    {
        public OrderMapping()
        {
            CreateMap<Entities.Order, DTOs.OrderDto>().ReverseMap();
            CreateMap<DTOs.OrderItemDto, Entities.OrderItem>().ReverseMap();
            CreateMap<DTOs.CreateOrderDto, Entities.Order>().ReverseMap();
            CreateMap<DTOs.AddressDto, Entities.Address>().ReverseMap();
            CreateMap<DTOs.PaymentDto, PaymentMessage>().ReverseMap();


        }
    }
}
