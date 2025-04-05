﻿using MassTransit;
using Shared.Interfaces;

namespace Shared.Events
{
    public class StockReservedRequestForPayment(Guid correlationId) : IStockReservedRequestForPayment
    {
        public IList<OrderItemMessage> OrderItems { get; set; }
        public PaymentMessage Payment { get; set; }

        public Guid CorrelationId { get; } = correlationId;
    }
}
