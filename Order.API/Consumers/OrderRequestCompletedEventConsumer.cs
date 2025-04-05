using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.API.Enums;
using Shared.Interfaces;

namespace Order.API.Consumers
{
    public class OrderRequestCompletedEventConsumer(AppDbContext _context, ILogger<OrderRequestCompletedEventConsumer> _logger) : IConsumer<IOrderRequestCompletedEvent>
    {
        public async Task Consume(ConsumeContext<IOrderRequestCompletedEvent> context)
        {
            var order = await _context.Orders.FindAsync(context.Message.OrderId);
            if (order != null)
            {
                order.Status = Enum.GetName(OrderStatus.Completed);
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Payment completed for OrderId: {context.Message.OrderId}");
            }
            else
            {
                _logger.LogError($"Order with OrderId: {context.Message.OrderId} not found");
            }
        }
    }
}
