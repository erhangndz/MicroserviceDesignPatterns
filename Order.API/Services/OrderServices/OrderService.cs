using AutoMapper;
using MassTransit.Configuration;
using Microsoft.AspNetCore.Http.HttpResults;
using Order.API.DTOs;
using Order.API.Repositories;

namespace Order.API.Services.OrderServices
{
    public class OrderService(IRepository<Entities.Order> orderRepository, IMapper mapper) : IOrderService
    {
        public async Task CreateOrderAsync(Entities.Order order)
        {
            await orderRepository.AddAsync(order);
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var entity = await orderRepository.GetByIdAsync(id);
            return mapper.Map<OrderDto>(entity);
        }

        public async Task<List<OrderDto>> GetOrdersAsync()
        {
            var values = await orderRepository.GetAllAsync();
            return mapper.Map<List<OrderDto>>(values);
        }

        public async Task UpdateOrderAsync(OrderDto orderDto)
        {
            var entity = mapper.Map<Entities.Order>(orderDto);
            await orderRepository.UpdateAsync(entity);
        }

        public async Task<IResult> DeleteOrderAsync(int id)
        {
            var entity = await orderRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return Results.NotFound();
            }
            await orderRepository.DeleteAsync(id);
            return Results.NoContent();
        }
    }
}
