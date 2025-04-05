using MassTransit;

namespace Shared.Interfaces
{
    public interface IPaymentFailedEvent: CorrelatedBy<Guid>
    {
       
        public string CustomerId { get; set; }
        public string Reason { get; set; }
        public IList<OrderItemMessage> OrderItems { get; set; }
    }
    
}
