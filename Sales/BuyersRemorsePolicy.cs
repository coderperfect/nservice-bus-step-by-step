using Messages.Commands;
using NServiceBus.Logging;
using Sales.Messages.Events;

namespace Sales;

class BuyersRemorsePolicy : Saga<BuyersRemorseState>,
    IAmStartedByMessages<PlaceOrder>,
    IHandleMessages<CancelOrder>,
    IHandleTimeouts<BuyersRemorseIsOver>
{
    static ILog log = LogManager.GetLogger<BuyersRemorsePolicy>();

    protected override void ConfigureHowToFindSaga(SagaPropertyMapper<BuyersRemorseState> mapper)
    {
        mapper.MapSaga(saga => saga.OrderId)
            .ToMessage<PlaceOrder>(message => message.OrderId)
            .ToMessage<CancelOrder>(message => message.OrderId);
    }

    public async Task Handle(PlaceOrder message, IMessageHandlerContext context)
    {
        log.Info($"Received PlaceOrder, OrderId = {message.OrderId}");
        Data.OrderId = message.OrderId;

        log.Info($"Starting cool down period for order #{Data.OrderId}.");
        await RequestTimeout(context, TimeSpan.FromSeconds(20), new BuyersRemorseIsOver());
    }

    public Task Handle(CancelOrder message, IMessageHandlerContext context)
    {
        log.Info($"Order #{message.OrderId} was cancelled.");

        MarkAsComplete();

        return Task.CompletedTask;
    }

    public async Task Timeout(BuyersRemorseIsOver timeout, IMessageHandlerContext context)
    {
        log.Info($"Cooling down period for order #{Data.OrderId} has elapsed.");

        var orderPlaced = new OrderPlaced
        {
            OrderId = Data.OrderId
        };

        await context.Publish(orderPlaced);

        MarkAsComplete();
    }
}

public class BuyersRemorseState : ContainSagaData
{
    public string OrderId { get; set; }
}

class BuyersRemorseIsOver
{
}