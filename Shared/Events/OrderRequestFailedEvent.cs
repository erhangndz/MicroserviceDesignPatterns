using Shared.Interfaces;

namespace Shared.Events
{
    public class OrderRequestFailedEvent(Guid correlationId) : IOrderRequestFailedEvent
    {
        public int OrderId { get; set; }
        public string Reason { get; set; }
        public Guid CorrelationId { get; } = correlationId;
    }
}
