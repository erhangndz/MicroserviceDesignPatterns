using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace Shared.Interfaces
{
    public interface IStockReservedEvent: CorrelatedBy<Guid>
    {
   
        public IList<OrderItemMessage> OrderItems { get; set; }
  
    }
}
