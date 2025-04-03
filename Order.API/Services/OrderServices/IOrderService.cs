using Order.API.DTOs;

namespace Order.API.Services.OrderServices
{
    public interface IOrderService
    {
        Task CreateOrderAsync(Entities.Order order);
        Task<OrderDto> GetOrderByIdAsync(int id);
        Task<List<OrderDto>> GetOrdersAsync();
        Task UpdateOrderAsync(OrderDto orderDto);
        Task<IResult> DeleteOrderAsync(int id);
    }
}
