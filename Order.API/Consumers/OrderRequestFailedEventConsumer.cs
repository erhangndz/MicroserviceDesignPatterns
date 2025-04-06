using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.API.Enums;
using Shared.Interfaces;

namespace Order.API.Consumers
{
    public class OrderRequestFailedEventConsumer(AppDbContext _context, ILogger<OrderRequestFailedEventConsumer> _logger) : IConsumer<IOrderRequestFailedEvent>
    {
        public async Task Consume(ConsumeContext<IOrderRequestFailedEvent> context)
        {
            var order = await _context.Orders.FindAsync(context.Message.OrderId);
            if (order != null)
            {
                order.Status = Enum.GetName(OrderStatus.Failed);
                order.FailMessage = context.Message.Reason;
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Order Failed for OrderId: {context.Message.OrderId}");
            }
            else
            {
                _logger.LogError($"Order with OrderId: {context.Message.OrderId} not found");
            }
        }
    }
}
