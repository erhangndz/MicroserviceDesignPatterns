using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SagaStateMachineWorkerService.Models
{
    public class OrderStateMap: SagaClassMap<OrderStateInstance>
    {
        protected override void Configure(EntityTypeBuilder<OrderStateInstance> entity, ModelBuilder model)
        {
            entity.Property(x => x.CustomerId).HasMaxLength(256);
            
        }
    }
}
