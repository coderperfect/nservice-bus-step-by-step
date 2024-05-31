namespace Shipping.Messages.Events;

public class ShipmentFailed : IEvent
{
    public string OrderId { get; set; }
}