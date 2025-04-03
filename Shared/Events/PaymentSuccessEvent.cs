namespace Shared.Events
{
    public class PaymentSuccessEvent
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }

    }
}
