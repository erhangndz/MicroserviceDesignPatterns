using MassTransit;

namespace Shared.Interfaces
{
    public interface IOrderRequestFailedEvent: CorrelatedBy<Guid>
    {
        public int OrderId { get; set; }
        public string Reason { get; set; }
    }
   
}
