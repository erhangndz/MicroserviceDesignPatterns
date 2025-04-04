using MassTransit;
using Order.API.Enums;
using Shared.Events;

namespace Order.API.Consumers
{
    public class PaymentFailedEventConsumer(AppDbContext _context, ILogger<PaymentFailedEventConsumer> _logger) : IConsumer<PaymentFailedEvent>
    {
        public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
        {
           var order = await _context.Orders.FindAsync(context.Message.OrderId);
            if (order != null)
            {
                order.Status = Enum.GetName(OrderStatus.Failed);
                order.FailMessage = context.Message.FailMessage;
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Payment failed for OrderId: {context.Message.OrderId}");
            }
            else
            {
                _logger.LogError($"Order with OrderId: {context.Message.OrderId} not found");
            }
        }
    }
}
