using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Interfaces;

namespace Shared.Events
{
    public class StockReservedEvent(Guid correlaitonId): IStockReservedEvent
    {
 

        public IList<OrderItemMessage> OrderItems { get; set; } 
        public Guid CorrelationId { get; } = correlaitonId;
       
    }
}
