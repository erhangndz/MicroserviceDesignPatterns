using MassTransit;
using Payment.API.Consumers;
using Shared;
using Shared.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMassTransit(config =>
{


    config.AddConsumer<StockReservedRequestForPaymentConsumer>();
    
    config.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration.GetConnectionString("RabbitMQ"));
       

        cfg.ReceiveEndpoint(RabbitMQSettingsConst.StockReservedRequestForPaymentQueueName, e =>
        {
            e.ConfigureConsumer<StockReservedRequestForPaymentConsumer>(context);
        });
    });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
