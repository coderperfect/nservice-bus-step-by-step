using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Billing.Messages.Events;
using NServiceBus.Logging;

namespace Shipping
{
    public class OrderBilledHandler : IHandleMessages<OrderBilled>
    {
        static ILog log = LogManager.GetLogger<OrderBilledHandler>();

        public Task Handle(OrderBilled message, IMessageHandlerContext context)
        {
            log.Info($"Received OrderBilled for Order {message.OrderId}, Should we ship now?");

            return Task.CompletedTask;
        }
    }
}
