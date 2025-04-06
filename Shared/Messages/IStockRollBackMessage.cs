namespace Shared.Messages
{
    public interface IStockRollBackMessage
    {
     
        public IList<OrderItemMessage> OrderItems { get; set; }
    }
}
