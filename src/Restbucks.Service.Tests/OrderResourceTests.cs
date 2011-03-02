using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using NUnit.Framework;
using Restbucks.Service.Activities;
using Restbucks.Service.Domain;
using Restbucks.Service.Mappers;
using Restbucks.Service.Representations;
using Restbucks.Service.Resources;

namespace Restbucks.Service.Tests
{
    [TestFixture]
    public class OrderResourceTests
    {
        [Test]
        public void Create_should_return_new_order_representation()
        {
            var sut = new OrderResource(new CreateOrderActivity(new InMemoryOrderRepository(), new OrderRepresentationMapper(new ItemRepresentationMapper())));
            var requestBody = new OrderRepresentation()
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
            var result = sut.Create(requestBody,
                       new HttpRequestMessage(HttpMethod.Post, "http://restbucks.net/order"),
                       new HttpResponseMessage());

            Assert.AreEqual(requestBody.Location, result.Location);
        }
    }
}
