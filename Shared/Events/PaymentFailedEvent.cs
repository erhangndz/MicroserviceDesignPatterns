using Shared.Interfaces;

namespace Shared.Events
{
    public class PaymentFailedEvent(Guid correlationId) : IPaymentFailedEvent
    {
        public string CustomerId { get; set; }
        public string Reason { get; set; }
        public IList<OrderItemMessage> OrderItems { get; set; }

        public Guid CorrelationId { get; } = correlationId;
    }
}
