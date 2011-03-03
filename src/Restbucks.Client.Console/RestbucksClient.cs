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
    }
}