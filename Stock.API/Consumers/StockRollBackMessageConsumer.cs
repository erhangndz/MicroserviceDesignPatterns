using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared.Messages;
using Stock.API.Context;

namespace Stock.API.Consumers
{
    public class StockRollBackMessageConsumer(AppDbContext _context, ILogger<StockRollBackMessageConsumer> _logger) : IConsumer<IStockRollBackMessage>
    {
        public async Task Consume(ConsumeContext<IStockRollBackMessage> context)
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

            _logger.LogInformation($"Payment failed , Stock was released");
        }
    }
}

