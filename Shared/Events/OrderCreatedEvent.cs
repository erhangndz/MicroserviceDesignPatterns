using Shared.Interfaces;

namespace Shared.Events
{
    public class OrderCreatedEvent(Guid correlationId) : IOrderCreatedEvent
    {
        public IList<OrderItemMessage> OrderItems { get; set; }

        public Guid CorrelationId { get; } = correlationId;
    }
}
