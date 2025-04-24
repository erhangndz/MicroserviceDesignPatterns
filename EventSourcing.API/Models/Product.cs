using System.ComponentModel.DataAnnotations.Schema;

namespace EventSourcing.API.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int UserId { get; set; }





    }
}
