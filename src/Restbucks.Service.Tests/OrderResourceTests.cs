using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.ApplicationServer.Http;
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
        private OrderResource _sut;
        private InMemoryOrderRepository _repository;

        [Test]
        public void Create_should_return_new_order_representation()
        {
            var requestBody = CreateOrder();

            var responseMessage = _sut.Create(new HttpRequestMessage<OrderRepresentation>(requestBody, HttpMethod.Post, new Uri("http://restbucks.net/order"), new MediaTypeFormatter[] {}));
            var result = responseMessage.Content.ReadAs();

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
            var requestBody = CreateOrder();

            var responseMessage = _sut.Create(new HttpRequestMessage<OrderRepresentation>(requestBody, HttpMethod.Post, new Uri("http://restbucks.net/order"), new MediaTypeFormatter[] { }));
            var createResult = responseMessage.Content.ReadAs();

            var parts = createResult.SelfLink.Split('/');

            responseMessage = _sut.Get(parts.Last(),
                                    new HttpRequestMessage(HttpMethod.Get, createResult.SelfLink));

            var getResult = responseMessage.Content.ReadAs();

            Assert.AreEqual(getResult.Cost, createResult.Cost);
        }

        [Test]
        public void Canceling_paid_order_should_return_405()
        {
            var order = new Order(Location.InStore, new[] {new Item(Drink.Espresso, Size.Medium, Milk.Semi)});
            order.Pay(new PaymentInformation(1, "", "", 12, 12));
            var id = _repository.Store(order);

            var responseMessage = _sut.Cancel(id.ToString(), new HttpRequestMessage(HttpMethod.Post, "http://restbucks.net/order"));

            Assert.AreEqual(HttpStatusCode.MethodNotAllowed, responseMessage.StatusCode);
        }

        [Test]
        public void Canceling_not_existent_order_should_return_404()
        {
            var responseMessage =  _sut.Cancel("13", new HttpRequestMessage(HttpMethod.Post, "http://restbucks.net/order"));

            Assert.AreEqual(HttpStatusCode.NotFound, responseMessage.StatusCode);
        }

        [Test]
        public void Canceling_unpaid_order_should_remove_it_from_repository()
        {
            var order = new Order(Location.InStore, new[] { new Item(Drink.Espresso, Size.Large, Milk.Semi) });
            var id = _repository.Store(order);

            _sut.Cancel(id.ToString(), new HttpRequestMessage(HttpMethod.Post, "http://restbucks.net/order"));

            Assert.IsNull(_repository.FindById(id));
        }

        [SetUp]
        public void Initialize()
        {
            RestbucksResources.BaseAddress = "http://restbucks.net";
            _repository = new InMemoryOrderRepository();
            var mapper = new OrderRepresentationMapper(new ItemRepresentationMapper());
            _sut = new OrderResource(
                new CreateOrderActivity(_repository, mapper),
                new ReadOrderActivity(_repository, mapper),
                new RemoveOrderActivity(_repository, mapper));
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
