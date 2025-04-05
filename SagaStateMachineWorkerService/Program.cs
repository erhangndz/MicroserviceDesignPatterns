using MassTransit;
using Microsoft.EntityFrameworkCore;
using SagaStateMachineWorkerService;
using SagaStateMachineWorkerService.Models;
using Shared;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddMassTransit(cfg =>
{
    cfg.AddSagaStateMachine<OrderStateMachine, OrderStateInstance>()
        .EntityFrameworkRepository(opt =>
    {
        opt.AddDbContext<DbContext,OrderStateDbContext>((sp, options) =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
        });
    });

    cfg.UsingRabbitMq((context, config) =>
    {
        config.Host(builder.Configuration.GetConnectionString("RabbitMQ"));

        config.ReceiveEndpoint(RabbitMQSettingsConst.OrderSagaQueueName, e =>
        {
            e.ConfigureSaga<OrderStateInstance>(context);
        });
    });
});






var host = builder.Build();
host.Run();
