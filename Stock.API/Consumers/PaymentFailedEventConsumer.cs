using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared.Events;
using Stock.API.Context;

namespace Stock.API.Consumers
{
    public class PaymentFailedEventConsumer(AppDbContext _context, ILogger<PaymentFailedEventConsumer> _logger): IConsumer<PaymentFailedEvent>
    {
        public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
        {
            foreach (var item in context.Message.OrderItems)
            {
                var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId);
                if (stock != null)
                {
                    stock.Count += item.Quantity;
                    _context.Stocks.Update(stock);
                  
                }
              
            }
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Payment failed for OrderId: {context.Message.OrderId}, Stock was released");
        }
    }
}
