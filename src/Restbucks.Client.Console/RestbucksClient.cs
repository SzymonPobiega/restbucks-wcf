using System.Net.Http;
using System.Net.Http.Headers;
using System.Xml.Serialization;
using Microsoft.ApplicationServer.Http;
using Restbucks.Service;
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
            var httpClient = GetHttpClient();
            var content = new ObjectContent<OrderRepresentation>(order, new [] {new RestbucksMediaTypeFormatter()});
            content.Headers.ContentType = new MediaTypeHeaderValue(RepresentationBase.RestbucksMediaType);
            var responseMessage = httpClient.Post(_baseUri+"/order", content);
            var responseContent = responseMessage.Content.ReadAs<OrderRepresentation>(new [] {new RestbucksMediaTypeFormatter()});
            return responseContent;
        }

        public OrderRepresentation GetOrder(string orderUri)
        {
            var httpClient = GetHttpClient();
            var responseMessage = httpClient.Get(orderUri);
            var responseContent = responseMessage.Content.ReadAs<OrderRepresentation>(new[] { new RestbucksMediaTypeFormatter() });
            return responseContent;
        }

        public PaymentRepresentation PayForOrder(string paymentUri, PaymentRepresentation payment)
        {
            var httpClient = GetHttpClient(_baseUri);
            var content = new ObjectContent<PaymentRepresentation>(payment, new[] { new RestbucksMediaTypeFormatter() });
            content.Headers.ContentType = new MediaTypeHeaderValue(RepresentationBase.RestbucksMediaType);
            var responseMessage = httpClient.Put(paymentUri, content);
            var responseContent = responseMessage.Content.ReadAs<PaymentRepresentation>(new [] {new RestbucksMediaTypeFormatter()});
            return responseContent;
        }

        public ReceiptRepresentation GetReceipt(string receiptUri)
        {
            var httpClient = GetHttpClient();       
            var responseMessage = httpClient.Get(receiptUri);
            var responseContent = responseMessage.Content.ReadAs<ReceiptRepresentation>(new [] {new RestbucksMediaTypeFormatter()});
            return responseContent;
        }

        public void TakeCoffee(string receiptUri)
        {
            var httpClient = GetHttpClient();
            httpClient.Delete(receiptUri);
        }

        private static HttpClient GetHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(RepresentationBase.RestbucksMediaType));
            return httpClient;
        }

        private static HttpClient GetHttpClient(string baseUri)
        {
            var httpClient = new HttpClient(baseUri);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(RepresentationBase.RestbucksMediaType));
            return httpClient;
        }
    }
}