using System.ComponentModel.DataAnnotations.Schema;

namespace Order.API.DTOs;

public class OrderItemDto
{
    public int ProductId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

  
    public decimal TotalPrice => Quantity* Price;
}