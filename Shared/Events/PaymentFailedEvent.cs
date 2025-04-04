namespace Shared.Events
{
    public class PaymentFailedEvent
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public string FailMessage { get; set; }

        public IList<OrderItemMessage> OrderItems { get; set; } = new List<OrderItemMessage>();
    }
}
