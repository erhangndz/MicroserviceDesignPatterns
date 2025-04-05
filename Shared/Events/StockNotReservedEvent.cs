using Shared.Interfaces;

namespace Shared.Events
{
    public class StockNotReservedEvent(Guid correlationId): IStockNotReservedEvent
    {

        public string Reason { get; set; }

        public Guid CorrelationId { get; } = correlationId;
    }
}
