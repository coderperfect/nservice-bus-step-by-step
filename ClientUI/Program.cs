using Messages.Commands;
using NServiceBus.Logging;

namespace ClientUI;
class Program
{
    static ILog log = LogManager.GetLogger<Program>();

    static async Task Main()
    {
        Console.Title = "Client UI";

        var endpointConfiguration = new EndpointConfiguration("ClientUI");
        endpointConfiguration.UseSerialization<SystemJsonSerializer>();

        var transport = endpointConfiguration.UseTransport<LearningTransport>();
        var routing = transport.Routing();
        routing.RouteToEndpoint(typeof(PlaceOrder), "Sales");

        var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

        await RunLoop(endpointInstance).ConfigureAwait(false);

        await endpointInstance.Stop().ConfigureAwait(false);
    }

    static async Task RunLoop(IEndpointInstance endpointInstance)
    {
        while (true)
        {
            log.Info("Press 'P' to place an order, or 'Q' to quit.");
            var key = Console.ReadKey();
            Console.WriteLine();

            switch (key.Key)
            {
                case ConsoleKey.P:
                    var command = new PlaceOrder
                    {
                        OrderId = Guid.NewGuid().ToString()
                    };

                    log.Info($"Sending PlaceOrder command, OrderId = {command.OrderId}");

                    await endpointInstance.Send(command).ConfigureAwait(false);

                    break;
                case ConsoleKey.Q:
                    return;

                default:
                    log.Info("Unknown input. Please try again.");
                    break;
            }
        }
    }
}
