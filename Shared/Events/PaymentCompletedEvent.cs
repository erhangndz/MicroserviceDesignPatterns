using Shared.Interfaces;

namespace Shared.Events
{
    public class PaymentCompletedEvent(Guid correlationId) : IPaymentCompletedEvent
    {
        public string CustomerId { get; set; }

        public Guid CorrelationId { get; } = correlationId;
    }
}
