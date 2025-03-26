using Order.API.Entities;

namespace Order.API.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public Address Address { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public string FailMessage { get; set; }
        public decimal TotalPrice { get; set; }
        public IList<OrderItemDto> Items { get; set; }
    }
}
