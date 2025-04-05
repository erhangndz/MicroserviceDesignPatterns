using MassTransit;

namespace Shared.Interfaces
{
    public interface IPaymentCompletedEvent: CorrelatedBy<Guid>
    {
  
        public string CustomerId { get; set; }
    }
}
