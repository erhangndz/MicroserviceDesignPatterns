using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Events
{
    public class StockReservedEvent
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public PaymentMessage Payment { get; set; }

        public IList<OrderItemMessage> OrderItems { get; set; } = new List<OrderItemMessage>();
    }
}
