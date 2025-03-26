using Order.API.Entities;
using Order.API.Enums;

namespace Order.API.DTOs
{
    public class CreateOrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }= DateTime.Now;
        public string Status { get; set; } = Enum.GetName(OrderStatus.Suspended);
        public string FailMessage { get; set; }
        public string CustomerId { get; set; }

        public List<OrderItemDto> Items { get; set; }

        public PaymentDto Payment { get; set; }



      
    }
}
 