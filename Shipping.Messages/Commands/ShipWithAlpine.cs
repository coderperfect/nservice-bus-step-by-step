namespace Shipping.Messages.Commands;

public class ShipWithAlpine : ICommand
{
    public string OrderId { get; set; }
}