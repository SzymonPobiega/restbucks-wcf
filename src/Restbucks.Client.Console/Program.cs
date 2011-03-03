using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Net.Http;
using Restbucks.Service.Domain;
using Restbucks.Service.Representations;

namespace Restbucks.Client.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RestbucksClient("http://localhost:52836");
            System.Console.WriteLine("Press enter to send order");
            System.Console.ReadLine();
            var order = client.CreateOrder(CreateOrder());
            System.Console.WriteLine("Order total: {0}", order.Cost);
            System.Console.WriteLine("Press enter to check order state");
            order = client.GetOrder(order.SelfLink);
            System.Console.WriteLine("Order state: {0}", order.Status);
            System.Console.WriteLine("Press enter to pay for the order");
            System.Console.ReadLine();
            var payment = new PaymentRepresentation
                              {
                                  Amount = 2.8m,
                                  CardholderName = "Szymon",
                                  CardNumber = "XXX",
                                  ExpiryMonth = 12,
                                  ExpiryYear = 12
                              };
            payment = client.PayForOrder(order.PaymentLink, payment);
            System.Console.WriteLine("Press enter to get the receipt");
            System.Console.ReadLine();
            var receipt = client.GetReceipt(payment.ReceiptLink);
            System.Console.WriteLine("You paid ${0} at {1} UTC", receipt.AmountPaid, receipt.PaymentDate);
            System.Console.WriteLine("Press enter to complete order");
            System.Console.ReadLine();
            client.TakeCoffee(receipt.CompleteLink);
            System.Console.WriteLine("Press enter to exit");
            System.Console.ReadLine();
        }

        private static OrderRepresentation CreateOrder()
        {
            return new OrderRepresentation()
            {
                Location = Location.InStore,
                Items = new List<ItemRepresentation>()
                                       {
                                           new ItemRepresentation()
                                               {
                                                   Drink = Drink.Espresso,
                                                   Milk = Milk.Semi,
                                                   Size = Size.Medium
                                               },
                                           new ItemRepresentation()
                                               {
                                                   Drink = Drink.Cappuccino,
                                                   Milk = Milk.Skim,
                                                   Size = Size.Large
                                               }
                                       }
            };
        }
    }
}
