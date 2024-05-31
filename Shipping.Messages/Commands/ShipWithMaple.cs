namespace Shipping.Messages.Commands;

public class ShipWithMaple : ICommand
{
    public string OrderId { get; set; }
}