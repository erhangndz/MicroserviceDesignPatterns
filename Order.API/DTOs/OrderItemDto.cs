using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Order.API.DTOs;

public class OrderItemDto
{
    public int ProductId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    
}