using MassTransit;
using Order.API.Enums;
using Shared.Events;

namespace Order.API.Consumers
{
    public class StockNotReservedEventConsumer(AppDbContext _context, ILogger<StockNotReservedEventConsumer> _logger): IConsumer<StockNotReservedEvent>
    {
        public async Task Consume(ConsumeContext<StockNotReservedEvent> context)
        {
          var order = await _context.Orders.FindAsync(context.Message.OrderId);
            if (order != null)
            {
                order.Status = Enum.GetName(OrderStatus.Failed);
                order.FailMessage = context.Message.FailMessage;
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Stock not reserved for OrderId: {context.Message.OrderId}");
            }
            else
            {
                _logger.LogError($"Order with OrderId: {context.Message.OrderId} not found");
            }
        }
    }
}
