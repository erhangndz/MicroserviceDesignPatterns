using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Order.API.DTOs;
using Order.API.Services.OrderServices;
using Shared;
using Shared.Events;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IOrderService orderService,IPublishEndpoint publishEndpoint) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto createOrderDto)
        {
           var result =  await orderService.CreateOrderAsync(createOrderDto);

            var orderCreatedEvent = new OrderCreatedEvent
            {
                
                CustomerId = createOrderDto.CustomerId,
                OrderId = result.Id,
                Payment = new PaymentMessage
                {
                    CardholdersName = createOrderDto.Payment.CardholdersName,
                    CardNumber = createOrderDto.Payment.CardNumber,
                    ExpiryDate = createOrderDto.Payment.ExpiryDate,
                    CVV = createOrderDto.Payment.CVV,
                    TotalPrice = createOrderDto.Items.Sum(x => x.Price * x.Quantity)

                }

            };

            createOrderDto.Items.ForEach(x =>
            {
                orderCreatedEvent.OrderItems.Add(new OrderItemMessage
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity
                });
            });

            await publishEndpoint.Publish(orderCreatedEvent);

            return Ok();
        }
    }
}
