using MassTransit;
using Shared.Events;

namespace Payment.API.Consumers
{
    public class StockReservedEventConsumer(IPublishEndpoint _publishEndpoint,
                                            ILogger<StockReservedEventConsumer> _logger): IConsumer<StockReservedEvent>
    {
        public async Task Consume(ConsumeContext<StockReservedEvent> context)
        {
            var balance = 3000m;

            if (balance >= context.Message.Payment.TotalPrice)
            {
                _logger.LogInformation($"{context.Message.Payment.TotalPrice} TL was withdrawn from credit card for CustomerId:  {context.Message.CustomerId}");

                await _publishEndpoint.Publish(new PaymentCompletedEvent
                {
                    CustomerId = context.Message.CustomerId,
                    OrderId = context.Message.OrderId
                });
            }

            else
            {
                _logger.LogInformation($"Payment failed for CustomerId: {context.Message.CustomerId}, Not Enough Balance for{context.Message.Payment.TotalPrice} TL ");
                await _publishEndpoint.Publish(new PaymentFailedEvent
                {
                    CustomerId = context.Message.CustomerId,
                    OrderId = context.Message.OrderId,
                    OrderItems = context.Message.OrderItems,
                    FailMessage = $"Not Enough Balance for {context.Message.Payment.TotalPrice} TL"
                });
            }
        }
    }
   
}
