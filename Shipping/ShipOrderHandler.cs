using NServiceBus.Logging;
using Shipping.Messages.Commands;

namespace Shipping;

public class ShipOrderHandler : IHandleMessages<ShipOrder>
{
    static ILog log = LogManager.GetLogger<ShipOrderHandler>();

    public Task Handle(ShipOrder message, IMessageHandlerContext context)
    {
        log.Info($"Order {message.OrderId} shipped!");
        return Task.CompletedTask;
    }
}