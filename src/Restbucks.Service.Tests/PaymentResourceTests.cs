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
    public class PaymentResourceTests
    {
        private PaymentResource _sut;
        private InMemoryOrderRepository _repository;

        [Test]
        public void Pay_should_return_403_if_already_paid()
        {
            var order = CreateOrder();
            order.Pay(new PaymentInformation(1, "", "", 12, 12));
            var id = _repository.Store(order);
            var representation = CreatePayment();

            var responseMessage = _sut.Pay(id.ToString(), new HttpRequestMessage<PaymentRepresentation>(representation, HttpMethod.Put, new Uri("http://restbucks.net/payment/" + id), new MediaTypeFormatter[] {}));

            Assert.AreEqual(HttpStatusCode.Forbidden, responseMessage.StatusCode);
        }

        [Test]
        public void Pay_should_return_400_if_amount_does_not_match()
        {
            var order = CreateOrder();
            var id = _repository.Store(order);
            var representation = CreatePayment();
            representation.Amount = 2;

            var responseMessage = _sut.Pay(id.ToString(), new HttpRequestMessage<PaymentRepresentation>(representation, HttpMethod.Put, new Uri("http://restbucks.net/payment/" + id), new MediaTypeFormatter[] { }));

            Assert.AreEqual(HttpStatusCode.BadRequest, responseMessage.StatusCode);
        }

        [Test]
        public void Pay_should_return_400_if_payment_id_is_not_integer()
        {
            var order = CreateOrder();
            var id = _repository.Store(order);
            var representation = CreatePayment();

            var responseMessage = _sut.Pay("aaa", new HttpRequestMessage<PaymentRepresentation>(representation, HttpMethod.Put, new Uri("http://restbucks.net/payment/" + id), new MediaTypeFormatter[] { }));

            Assert.AreEqual(HttpStatusCode.BadRequest, responseMessage.StatusCode);
        }
        [Test]
        public void Pay_should_return_404_if_payment_id_is_invalid()
        {
            var order = CreateOrder();
            var id = _repository.Store(order);
            var representation = CreatePayment();

            var responseMessage = _sut.Pay("13", new HttpRequestMessage<PaymentRepresentation>(representation, HttpMethod.Put, new Uri("http://restbucks.net/payment/" + id), new MediaTypeFormatter[] { }));

            Assert.AreEqual(HttpStatusCode.NotFound, responseMessage.StatusCode);
        }

        [Test]
        public void Pay_should_return_200_if_unpaid()
        {
            var order = CreateOrder();
            var id = _repository.Store(order);
            var representation = CreatePayment();

            var responseMessage = _sut.Pay(id.ToString(), new HttpRequestMessage<PaymentRepresentation>(representation, HttpMethod.Put, new Uri("http://restbucks.net/payment/" + id), new MediaTypeFormatter[] { }));
            var result = responseMessage.Content.ReadAs();

            Assert.AreEqual(HttpStatusCode.Created, responseMessage.StatusCode);
            Assert.AreEqual("http://restbucks.net/order/1", result.OrderLink);
            Assert.AreEqual("http://restbucks.net/receipt/1", result.ReceiptLink);
        }

        private static Order CreateOrder()
        {
            return new Order(Location.InStore, new[] { new Item(Drink.Espresso, Size.Medium, Milk.Semi) });
        }

        private static PaymentRepresentation CreatePayment()
        {
            return new PaymentRepresentation
            {
                Amount = 1,
                CardholderName = "Szymon",
                CardNumber = "XXX",
                ExpiryMonth = 12,
                ExpiryYear = 12
            };
        }

        [SetUp]
        public void Initialize()
        {
            RestbucksResources.BaseAddress = "http://restbucks.net";
            _repository = new InMemoryOrderRepository();
            var mapper = new PaymentRepresentationMapper();
            _sut = new PaymentResource(new PaymentActivity(_repository, mapper));
        }
    }
}
