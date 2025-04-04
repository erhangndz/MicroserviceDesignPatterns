namespace Shared.Interfaces;

    public interface IOrderCreatedRequestEvent
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public IList<OrderItemMessage> OrderItems { get; set; }
        public PaymentMessage Payment { get; set; }
}

