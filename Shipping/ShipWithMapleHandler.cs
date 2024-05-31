using NServiceBus.Logging;
using Shipping.Messages;
using Shipping.Messages.Commands;

namespace Shipping;

class ShipWithMapleHandler : IHandleMessages<ShipWithMaple>
{
    static ILog log = LogManager.GetLogger<ShipWithMapleHandler>();

    const int MaximumTimeMapleMightRespond = 60;
    static Random random = new Random();

    public async Task Handle(ShipWithMaple message, IMessageHandlerContext context)
    {
        var waitingTime = random.Next(MaximumTimeMapleMightRespond);

        log.Info($"ShipWithMapleHandler: Delaying Order [{message.OrderId}] {waitingTime} seconds.");

        await Task.Delay(waitingTime * 1000);

        await context.Reply(new ShipmentAcceptedByMaple());
    }
}