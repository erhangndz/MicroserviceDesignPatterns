using Order.API.Enums;

namespace Order.API.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public Address Address { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public string FailMessage { get; set; }
        public IList<OrderItem> Items { get; set; }
    }
}
