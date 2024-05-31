using Billing.Messages.Events;
using NServiceBus.Logging;
using Sales.Messages.Events;
using Shipping.Messages.Commands;

namespace Shipping;

public class ShippingPolicy : Saga<ShippingPolicy.ShippingPolicyData>,
    IAmStartedByMessages<OrderPlaced>,
    IAmStartedByMessages<OrderBilled>
{
    static ILog log = LogManager.GetLogger<ShippingPolicy>();

    protected override void ConfigureHowToFindSaga(SagaPropertyMapper<ShippingPolicyData> mapper)
    {
        mapper.MapSaga(sagaData => sagaData.OrderId)
            .ToMessage<OrderPlaced>(msg => msg.OrderId)
            .ToMessage<OrderBilled>(msg => msg.OrderId);
    }

    public async Task Handle(OrderPlaced message, IMessageHandlerContext context)
    {
        log.Info($"Received OrderPlaced for Order {message.OrderId}");

        Data.IsOrderPlaced = true;
        await ProcessOrder(context);
    }

    public async Task Handle(OrderBilled message, IMessageHandlerContext context)
    {
        log.Info($"Received OrderBilled for Order {message.OrderId}");

        Data.IsOrderBilled = true;
        await ProcessOrder(context);
    }

    private async Task ProcessOrder(IMessageHandlerContext context)
    {
        if (Data.IsOrderPlaced && Data.IsOrderBilled)
        {
            await context.SendLocal(new ShipOrder { OrderId = Data.OrderId });
            MarkAsComplete();
        }
    }

    public class ShippingPolicyData : ContainSagaData
    {
        public string OrderId { get; set; }
        public bool IsOrderPlaced { get; set; }
        public bool IsOrderBilled { get; set; }
    }
}