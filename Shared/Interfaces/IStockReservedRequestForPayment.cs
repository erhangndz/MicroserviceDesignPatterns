﻿using MassTransit;

namespace Shared.Interfaces
{
    public interface IStockReservedRequestForPayment: CorrelatedBy<Guid>
    {
        public string CustomerId { get; set; }
        public IList<OrderItemMessage> OrderItems { get; set; }
        public PaymentMessage Payment { get; set; }
    }
}
