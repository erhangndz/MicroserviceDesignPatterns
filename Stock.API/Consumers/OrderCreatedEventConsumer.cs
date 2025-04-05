using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared.Events;
using Shared.Interfaces;
using Stock.API.Context;

namespace Stock.API.Consumers;

public class OrderCreatedEventConsumer(AppDbContext _context, ILogger<OrderCreatedEventConsumer> _logger, IPublishEndpoint _publishEndpoint) : IConsumer<IOrderCreatedEvent>
{
    public async Task Consume(ConsumeContext<IOrderCreatedEvent> context)
    {
        var stockResult = new List<bool>();

        foreach (var item in context.Message.OrderItems)
        {
            stockResult.Add(await _context.Stocks.AnyAsync(x => x.ProductId == item.ProductId && x.Count >= item.Quantity));
        }

        if (stockResult.All(x => x.Equals(true)))
        {
            foreach (var item in context.Message.OrderItems)
            {
                var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId);
                stock.Count -= item.Quantity;
                _context.Stocks.Update(stock);
            }
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Stock was reserved for Correlation Id: {context.Message.CorrelationId}");


           

            var stockReservedEvent = new StockReservedEvent(context.Message.CorrelationId)
            {

                OrderItems = context.Message.OrderItems,

            };
            await _publishEndpoint.Publish(stockReservedEvent);


        }
        else
        {
            await _publishEndpoint.Publish(new StockNotReservedEvent(context.Message.CorrelationId)
            {

                Reason = "Not Enough Stock"
            });

            _logger.LogInformation($"Not Enough Stock for Correlation Id :  {context.Message.CorrelationId} ");
        }
    }
}


