using Order.API.Entities;
using Order.API.Enums;

namespace Order.API.DTOs
{
    public class CreateOrderDto
    {
 

        public string CustomerId { get; set; }

        public List<OrderItemDto> OrderItems { get; set; }

        public PaymentDto Payment { get; set; }

        public AddressDto Address { get; set; }



      
    }
}
 