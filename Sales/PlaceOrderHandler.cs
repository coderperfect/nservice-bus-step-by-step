// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using Messages.Commands;
// using NServiceBus.Logging;
// using Sales.Messages.Events;
//
// namespace Sales;
//
// public class PlaceOrderHandler : IHandleMessages<PlaceOrder>
// {
//     static ILog log = LogManager.GetLogger<PlaceOrderHandler>();
//     static Random random = new Random();
//
//     public Task Handle(PlaceOrder message, IMessageHandlerContext context)
//     {
//         log.Info($"Received PlaceOder, OrderId: {message.OrderId}");
//
//         //throw new Exception("Boom");
//
//         //if (random.Next(0, 5) == 0)
//         //{
//         //    throw new Exception("Oops");
//         //}
//
//         var orderPlaced = new OrderPlaced
//         {
//             OrderId = message.OrderId
//         };
//
//         return context.Publish( orderPlaced );
//     }
// }
