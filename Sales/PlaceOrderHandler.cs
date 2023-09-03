﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messages.Commands;
using NServiceBus.Logging;

namespace Sales;

public class PlaceOrderHandler : IHandleMessages<PlaceOrder>
{
    static ILog log = LogManager.GetLogger<PlaceOrderHandler>();

    public Task Handle(PlaceOrder message, IMessageHandlerContext context)
    {
        log.Info($"Received PlaceOder, OrderId: {message.OrderId}");
        return Task.CompletedTask;
    }
}