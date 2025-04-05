using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;

namespace SagaStateMachineWorkerService.Models
{
    public class OrderStateDbContext(DbContextOptions options) : SagaDbContext(options)
    {
        protected override IEnumerable<ISagaClassMap> Configurations
        {
            get { yield return new OrderStateMap(); }
        }
    }
}
