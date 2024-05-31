﻿namespace Sales;

class Program
{
    static async Task Main()
    {
        Console.Title = "Sales";

        var endpointConfiguration = new EndpointConfiguration("Sales");
        endpointConfiguration.UseSerialization<SystemJsonSerializer>();
        var transport = endpointConfiguration.UseTransport<LearningTransport>();
        var persistence = endpointConfiguration.UsePersistence<LearningPersistence>();

        var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

        Console.WriteLine("Press Enter to exit.");
        Console.ReadLine();

        await endpointInstance.Stop().ConfigureAwait(false);
    }
}