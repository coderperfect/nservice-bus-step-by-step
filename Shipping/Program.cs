﻿using Shipping.Messages.Commands;

namespace Shipping;

class Program
{
    static async Task Main()
    {
        Console.Title = "Shipping";

        var endpointConfiguration = new EndpointConfiguration("Shipping");
        endpointConfiguration.UseSerialization<SystemJsonSerializer>();
        var transport = endpointConfiguration.UseTransport<LearningTransport>();
        var persistence = endpointConfiguration.UsePersistence<LearningPersistence>();

        var routing = transport.Routing();
        routing.RouteToEndpoint(typeof(ShipOrder), "Shipping");
        routing.RouteToEndpoint(typeof(ShipWithMaple), "Shipping");
        routing.RouteToEndpoint(typeof(ShipWithAlpine), "Shipping");

        var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

        Console.WriteLine("Press Enter to exit.");
        Console.ReadLine();

        await endpointInstance.Stop().ConfigureAwait(false);
    }
}