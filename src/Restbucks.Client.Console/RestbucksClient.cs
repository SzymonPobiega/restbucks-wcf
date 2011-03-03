using System.Net.Http;
using System.Net.Http.Headers;
using System.Xml.Serialization;
using Microsoft.Xml.Serialization;
using Restbucks.Service.Representations;

namespace Restbucks.Client.Console
{
    public class RestbucksClient
    {
        private readonly string _baseUri;

        public RestbucksClient(string baseUri)
        {
            _baseUri = baseUri;
        }

        public OrderRepresentation CreateOrder(OrderRepresentation order)
        {
            var httpClient = new HttpClient(_baseUri);
            var serializer = new XmlSerializer(typeof(OrderRepresentation));
            var content = order.ToContentUsingXmlSerializer(serializer);
            content.Headers.ContentType = new MediaTypeHeaderValue(RepresentationBase.RestbucksMediaType);
            var responseMessage = httpClient.Post("/order", content);
            var responseContent = responseMessage.Content.ReadAsXmlSerializable<OrderRepresentation>(serializer);
            return responseContent;
        }

        public OrderRepresentation GetOrder(string orderUri)
        {
            var httpClient = new HttpClient();
            var serializer = new XmlSerializer(typeof(OrderRepresentation));
            var responseMessage = httpClient.Get(orderUri);
            var responseContent = responseMessage.Content.ReadAsXmlSerializable<OrderRepresentation>(serializer);
            return responseContent;
        }

        public PaymentRepresentation PayForOrder(string paymentUri, PaymentRepresentation payment)
        {
            var httpClient = new HttpClient(_baseUri);
            var serializer = new XmlSerializer(typeof(PaymentRepresentation));
            var content = payment.ToContentUsingXmlSerializer(serializer);
            content.Headers.ContentType = new MediaTypeHeaderValue(RepresentationBase.RestbucksMediaType);
            var responseMessage = httpClient.Put(paymentUri, content);
            var responseContent = responseMessage.Content.ReadAsXmlSerializable<PaymentRepresentation>(serializer);
            return responseContent;
        }

        public ReceiptRepresentation GetReceipt(string receiptUri)
        {
            var httpClient = new HttpClient();
            var serializer = new XmlSerializer(typeof(ReceiptRepresentation));
            var responseMessage = httpClient.Get(receiptUri);
            var responseContent = responseMessage.Content.ReadAsXmlSerializable<ReceiptRepresentation>(serializer);
            return responseContent;
        }
    }
}