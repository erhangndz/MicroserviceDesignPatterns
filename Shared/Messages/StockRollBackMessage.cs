namespace Shared.Messages
{
    public class StockRollBackMessage: IStockRollBackMessage
    {
        public IList<OrderItemMessage> OrderItems { get; set; }
    }
}
