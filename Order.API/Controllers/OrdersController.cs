using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Order.API.DTOs;
using Order.API.Entities;
using Order.API.Enums;
using Order.API.Services.OrderServices;
using Shared;
using Shared.Events;
using Shared.Interfaces;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IOrderService orderService, ISendEndpointProvider _sendEndpointProvider) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto createOrderDto)
        {
            var newOrder = new Entities.Order
            {
                CustomerId = createOrderDto.CustomerId,
                Status = Enum.GetName(OrderStatus.Suspended),
                Address = new Address
                {
                    City = createOrderDto.Address.City,
                    District = createOrderDto.Address.District,
                    Line = createOrderDto.Address.Line
                },
                OrderDate = DateTime.Now
            };

            createOrderDto.OrderItems.ForEach(item =>
            {
                newOrder.Items.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    OrderId = newOrder.Id
                });
            });

            await orderService.CreateOrderAsync(newOrder);

            var orderCreatedRequestEvent = new OrderCreatedRequestEvent()
            {
                OrderId = newOrder.Id,
                CustomerId = newOrder.CustomerId,
                Payment = new PaymentMessage
                {
                    CardholdersName = createOrderDto.Payment.CardholdersName,
                    CardNumber = createOrderDto.Payment.CardNumber,
                    ExpiryDate  = createOrderDto.Payment.ExpiryDate,
                    CVV = createOrderDto.Payment.CVV,
                    TotalPrice = createOrderDto.OrderItems.Sum(x => x.Price * x.Quantity)
                }

                
            };

            createOrderDto.OrderItems.ForEach(item =>
            {
                orderCreatedRequestEvent.OrderItems.Add(new OrderItemMessage
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                });
            });

            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri(RabbitMQSettingsConst.OrderSagaQueueName));

            await sendEndpoint.Send<IOrderCreatedRequestEvent>(orderCreatedRequestEvent);

            return Ok();
        }
    }
}
