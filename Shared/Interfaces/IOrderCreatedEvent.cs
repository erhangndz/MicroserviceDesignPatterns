using MassTransit;

namespace Shared.Interfaces
{
    public interface IOrderCreatedEvent: CorrelatedBy<Guid>
    {
        public IList<OrderItemMessage> OrderItems { get; set; }
    }
}
