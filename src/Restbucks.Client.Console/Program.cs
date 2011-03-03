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
