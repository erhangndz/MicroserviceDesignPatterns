using MassTransit;
using Order.API.Enums;
using Shared.Events;

namespace Order.API.Consumers
{
    public class PaymentCompletedEventConsumer(AppDbContext _context, ILogger<PaymentCompletedEventConsumer> _logger) : IConsumer<PaymentCompletedEvent>
    {
        public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
        {
            var order = await _context.Orders.FindAsync(context.Message.OrderId);
            if(order!= null)
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
