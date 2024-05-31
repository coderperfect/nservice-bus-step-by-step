// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using NServiceBus.Logging;
// using Sales.Messages.Events;
//
// namespace Shipping
// {
//     public class OrderPlacedHandler : IHandleMessages<OrderPlaced>
//     {
//         static ILog log = LogManager.GetLogger<OrderPlacedHandler>();
//
//         public Task Handle(OrderPlaced message, IMessageHandlerContext context)
//         {
//             log.Info($"Received OrderPlaced for Order {message.OrderId}, Should we ship now?");
//
//             return Task.CompletedTask;
//         }
//     }
// }
