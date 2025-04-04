using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Events;
using Stock.API.Context;

namespace Stock.API.Consumers;

public class OrderCreatedEventConsumer(AppDbContext _context, ILogger<OrderCreatedEventConsumer> _logger, ISendEndpointProvider _sendEndpointProvider, IPublishEndpoint _publishEndpoint) : IConsumer<OrderCreatedEvent>
{
    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var stockResult = new List<bool>();

        foreach (var item in context.Message.OrderItems)
        {
            stockResult.Add(await _context.Stocks.AnyAsync(x => x.ProductId == item.ProductId && x.Count >= item.Quantity));
        }

        if(stockResult.All(x=>x.Equals(true)))
        {
            foreach (var item in context.Message.OrderItems)
            {
                var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.ProductId == item.ProductId);
                stock.Count -= item.Quantity;
                _context.Stocks.Update(stock);
            }
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Stock was reserved for Customer Id: {context.Message.CustomerId}");
          

            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{RabbitMQSettingsConst.StockReservedEventQueueName}"));

            var stockReservedEvent = new StockReservedEvent
            {
                OrderId = context.Message.OrderId,
                CustomerId = context.Message.CustomerId,
                OrderItems = context.Message.OrderItems,
                Payment = context.Message.Payment
            };

            await sendEndpoint.Send(stockReservedEvent);


        }
        else
        {
            await _publishEndpoint.Publish(new StockNotReservedEvent()
            {
                OrderId = context.Message.OrderId,
                FailMessage = "Not Enough Stock"
            });

            _logger.LogInformation($"Not Enough Stock for Customer Id :  {context.Message.CustomerId} ");
        }
    }
}

