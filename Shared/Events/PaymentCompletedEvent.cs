namespace Shared.Events
{
    public class PaymentCompletedEvent
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }

    }
}
