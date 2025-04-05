﻿using MassTransit;
using Shared;
using Shared.Events;
using Shared.Interfaces;

namespace SagaStateMachineWorkerService.Models;

    public class OrderStateMachine : MassTransitStateMachine<OrderStateInstance>
    {
        public Event<IOrderCreatedRequestEvent> OrderCreatedRequestEvent { get; set; }
        public Event<IStockReservedEvent> StockReservedEvent { get; set; }

        public State OrderCreated { get; private set; }
        public State StockReserved { get; private set; }

        public OrderStateMachine()
        {
            InstanceState(x=>x.CurrentState);

            Event(() => OrderCreatedRequestEvent,
                c => 
                    c.CorrelateBy<int>(i=>i.OrderId,context=>context.Message.OrderId).SelectId(context=> Guid.NewGuid()));

            Initially(When(OrderCreatedRequestEvent).Then(context =>
            {
                context.Saga.CustomerId = context.Message.CustomerId;
                context.Saga.OrderId = context.Message.OrderId;
                context.Saga.CreatedDate = DateTime.Now;
                context.Saga.CardholdersName = context.Message.Payment.CardholdersName;
                context.Saga.CardNumber = context.Message.Payment.CardNumber;
                context.Saga.ExpiryDate = context.Message.Payment.ExpiryDate;
                context.Saga.CVV = context.Message.Payment.CVV;
                context.Saga.TotalPrice = context.Message.Payment.TotalPrice;


            })
                .Then(context =>
            {
                Console.WriteLine($"Order Created Request Event Before: {context.Saga}");
            })
                .Publish(context =>
                 new OrderCreatedEvent(context.Saga.CorrelationId)
                    {
                        OrderItems = context.Message.OrderItems
                    }
                )
                .TransitionTo(OrderCreated)
                .Then(context =>
            {
                Console.WriteLine($"Order Created Request Event After: {context.Saga}");
            }));

            During(OrderCreated,When(StockReservedEvent)
                .TransitionTo(StockReserved)
                .Send(new Uri($"queue:{RabbitMQSettingsConst.StockReservedRequestForPaymentQueueName}"), context => 
                    new StockReservedRequestForPayment(context.Saga.CorrelationId)
                {
                    
                  
                   OrderItems = context.Message.OrderItems,
                   Payment = new PaymentMessage
                   {
                       CardholdersName = context.Saga.CardholdersName,
                       CardNumber = context.Saga.CardNumber,
                       ExpiryDate = context.Saga.ExpiryDate,
                       CVV = context.Saga.CVV,
                       TotalPrice = context.Saga.TotalPrice

                   }
                    }


                )
                .Then(context =>
                {
                    Console.WriteLine($"Stock Reserved Event After: {context.Saga}");
                }));
        }

    }




