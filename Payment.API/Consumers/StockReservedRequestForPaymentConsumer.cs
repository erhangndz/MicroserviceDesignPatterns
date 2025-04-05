using MassTransit;
using Shared.Events;
using Shared.Interfaces;

namespace Payment.API.Consumers
{
    public class StockReservedRequestForPaymentConsumer(IPublishEndpoint _publishEndpoint,
        ILogger<StockReservedRequestForPaymentConsumer> _logger) : IConsumer<IStockReservedRequestForPayment>
    {
        public async Task Consume(ConsumeContext<IStockReservedRequestForPayment> context)
        {
            var balance = 3000m;

            if (balance >= context.Message.Payment.TotalPrice)
            {
                _logger.LogInformation($"{context.Message.Payment.TotalPrice} TL was withdrawn from credit card for CustomerId:  {context.Message.CustomerId}");

                await _publishEndpoint.Publish(new PaymentCompletedEvent(context.Message.CorrelationId)
                {
                    CustomerId = context.Message.CustomerId,
                    

                });
            }

            else
            {
                _logger.LogInformation($"Payment failed for CustomerId: {context.Message.CustomerId}, Not Enough Balance for{context.Message.Payment.TotalPrice} TL ");
                await _publishEndpoint.Publish(new PaymentFailedEvent(context.Message.CorrelationId)
                {
                    CustomerId = context.Message.CustomerId,
                    OrderItems = context.Message.OrderItems,
                    Reason = $"Not Enough Balance for {context.Message.Payment.TotalPrice} TL"
                });
            }
        }
    }
}
