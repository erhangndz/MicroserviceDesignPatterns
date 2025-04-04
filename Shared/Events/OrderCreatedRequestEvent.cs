using Shared.Interfaces;

namespace Shared.Events
{
    public class OrderCreatedRequestEvent: IOrderCreatedRequestEvent
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public IList<OrderItemMessage> OrderItems { get; set; } = new List<OrderItemMessage>();
        public PaymentMessage Payment { get; set; }
    }
}
