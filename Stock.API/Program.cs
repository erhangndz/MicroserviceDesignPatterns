using MassTransit;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Shared;
using Stock.API.Consumers;
using Stock.API.Context;
using Stock.API.Entities;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMassTransit(config =>
{



    config.AddConsumer<OrderCreatedEventConsumer>();
    config.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration.GetConnectionString("RabbitMQ"));
        cfg.ReceiveEndpoint(RabbitMQSettingsConst.StockOrderCreatedEventQueueName, e =>
        {
            e.ConfigureConsumer<OrderCreatedEventConsumer>(context);
        });
    });
});
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseInMemoryDatabase("StockDb");
});

using var context = builder.Services.BuildServiceProvider().GetRequiredService<AppDbContext>();

context.Stocks.Add(new Stock.API.Entities.Stock { Id = 1, ProductId = 1, Count = 100 });
context.Stocks.Add(new Stock.API.Entities.Stock { Id = 2, ProductId = 2, Count = 100 });
context.SaveChanges();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
