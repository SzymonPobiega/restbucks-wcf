using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using NUnit.Framework;
using Restbucks.Service.Activities;
using Restbucks.Service.Domain;
using Restbucks.Service.Infrastructure;
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
            var sut = CreateResource();
            var requestBody = CreateOrder();

            var result = sut.Create(requestBody,
                       new HttpRequestMessage(HttpMethod.Post, "http://restbucks.net/order"),
                       new HttpResponseMessage());

            Assert.AreEqual(requestBody.Location, result.Location);
            Assert.AreEqual(2.8m, result.Cost);
            Assert.AreEqual("http://restbucks.net/order/1",result.UpdateLink);
            Assert.AreEqual("http://restbucks.net/order/1", result.SelfLink);
            Assert.AreEqual("http://restbucks.net/order/1", result.CancelLink);
            Assert.AreEqual("http://restbucks.net/payment/1", result.PaymentLink);
        }

        [Test]
        public void Get_should_return_the_same_representation_as_create_is_order_status_is_unpaid()
        {
            var sut = CreateResource();
            var requestBody = CreateOrder();

            var createResult = sut.Create(requestBody,
                       new HttpRequestMessage(HttpMethod.Post, "http://restbucks.net/order"),
                       new HttpResponseMessage());

            var parts = createResult.SelfLink.Split('/');

            var getResult = sut.Get(parts.Last(),
                                    new HttpRequestMessage(HttpMethod.Get, createResult.SelfLink),
                                    new HttpResponseMessage());

            Assert.AreEqual(getResult.Cost, createResult.Cost);
        }

        private static OrderResource CreateResource()
        {
            var repository = new InMemoryOrderRepository();
            var mapper = new OrderRepresentationMapper(new ItemRepresentationMapper());
            return new OrderResource(
                new CreateOrderActivity(repository, mapper), 
                new ReadOrderActivity(repository, mapper));
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
