using MassTransit;

namespace Shared.Interfaces
{
    public interface IStockNotReservedEvent: CorrelatedBy<Guid>
    {

        public string Reason { get; set; }
       
    }
}
