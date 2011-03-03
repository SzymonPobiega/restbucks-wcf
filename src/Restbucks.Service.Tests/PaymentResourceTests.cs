using System.Collections.Generic;
using System.Linq;
using System.Net;
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

            var responseMessage = new HttpResponseMessage();

            _sut.Pay(id.ToString(), representation,
                     new HttpRequestMessage(HttpMethod.Put, "http://restbucks.net/payment/" + id),
                     responseMessage);

            Assert.AreEqual(HttpStatusCode.Forbidden, responseMessage.StatusCode);
        }

        [Test]
        public void Pay_should_return_400_if_amount_does_not_match()
        {
            var order = CreateOrder();
            var id = _repository.Store(order);
            var representation = CreatePayment();
            representation.Amount = 2;

            var responseMessage = new HttpResponseMessage();

            _sut.Pay(id.ToString(), representation,
                     new HttpRequestMessage(HttpMethod.Put, "http://restbucks.net/payment/" + id),
                     responseMessage);

            Assert.AreEqual(HttpStatusCode.BadRequest, responseMessage.StatusCode);
        }

        [Test]
        public void Pay_should_return_400_if_payment_id_is_not_integer()
        {
            var order = CreateOrder();
            var id = _repository.Store(order);
            var representation = CreatePayment();

            var responseMessage = new HttpResponseMessage();

            _sut.Pay("aaa", representation,
                     new HttpRequestMessage(HttpMethod.Put, "http://restbucks.net/payment/" + id),
                     responseMessage);

            Assert.AreEqual(HttpStatusCode.BadRequest, responseMessage.StatusCode);
        }
        [Test]
        public void Pay_should_return_404_if_payment_id_is_invalid()
        {
            var order = CreateOrder();
            var id = _repository.Store(order);
            var representation = CreatePayment();

            var responseMessage = new HttpResponseMessage();

            _sut.Pay("13", representation,
                     new HttpRequestMessage(HttpMethod.Put, "http://restbucks.net/payment/" + id),
                     responseMessage);

            Assert.AreEqual(HttpStatusCode.NotFound, responseMessage.StatusCode);
        }

        [Test]
        public void Pay_should_return_200_if_unpaid()
        {
            var order = CreateOrder();
            var id = _repository.Store(order);
            var representation = CreatePayment();

            var responseMessage = new HttpResponseMessage();

            var responseBody = _sut.Pay(id.ToString(), representation,
                     new HttpRequestMessage(HttpMethod.Put, "http://restbucks.net/payment/" + id),
                     responseMessage);

            Assert.AreEqual(HttpStatusCode.Created, responseMessage.StatusCode);
            Assert.AreEqual("http://restbucks.net/order/1", responseBody.OrderLink);
            Assert.AreEqual("http://restbucks.net/receipt/1", responseBody.ReceiptLink);
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
            _repository = new InMemoryOrderRepository();
            var mapper = new PaymentRepresentationMapper();
            _sut = new PaymentResource(new PaymentActivity(_repository, mapper));
        }
    }
}
