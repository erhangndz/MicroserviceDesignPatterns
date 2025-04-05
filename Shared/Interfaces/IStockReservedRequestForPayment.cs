using MassTransit;

namespace Shared.Interfaces
{
    public interface IStockReservedRequestForPayment: CorrelatedBy<Guid>
    {
    
        public IList<OrderItemMessage> OrderItems { get; set; }
        public PaymentMessage Payment { get; set; }
    }
}
