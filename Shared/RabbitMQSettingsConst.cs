namespace Shared
{
    public class RabbitMQSettingsConst
    {
        public const string StockOrderCreatedEventQueueName = "stock-order-created-queue";
        public const string StockReservedEventQueueName = "stock-reserved-queue";
        public const string PaymentCompletedEventQueueName = "payment-completed-queue";
        public const string PaymentFailedEventQueueName = "payment-failed-queue";
        public const string StockPaymentFailedEventQueueName = "stock-payment-failed-queue";

        public const string StockNotReservedEventQueueName = "stock-not-reserved-queue";


    }
}
