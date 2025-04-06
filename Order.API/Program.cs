using System.Reflection;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.API;
using Order.API.Consumers;
using Order.API.Repositories;
using Order.API.Services.OrderServices;
using Scalar.AspNetCore;
using Shared;
using Shared.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<OrderRequestCompletedEventConsumer>();
    config.AddConsumer<OrderRequestFailedEventConsumer>();
    
    config.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration.GetConnectionString("RabbitMQ"));

        cfg.ReceiveEndpoint(RabbitMQSettingsConst.OrderRequestCompletedEventQueueName, e =>
        {
            e.ConfigureConsumer<OrderRequestCompletedEventConsumer>(context);
        });

        cfg.ReceiveEndpoint(RabbitMQSettingsConst.OrderRequestFailedEventQueueName, e =>
        {
            e.ConfigureConsumer<OrderRequestFailedEventConsumer>(context);
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
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
