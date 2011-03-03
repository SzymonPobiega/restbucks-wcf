using System;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.ServiceModel.Web;
using Restbucks.Service.Activities;
using Restbucks.Service.Representations;

namespace Restbucks.Service.Resources
{
    [ServiceContract]
    public class OrderResource
    {
        private readonly ICreateOrderActivity _createOrderActivity;
        private readonly IReadOrderActivity _readOrderActivity;

        public OrderResource(ICreateOrderActivity createOrderActivity, IReadOrderActivity readOrderActivity)
        {
            _createOrderActivity = createOrderActivity;
            _readOrderActivity = readOrderActivity;
        }

        [WebInvoke(
            Method = "POST", 
            UriTemplate = "/",
            RequestFormat = WebMessageFormat.Xml, 
            ResponseFormat = WebMessageFormat.Xml)]
        public OrderRepresentation Create(OrderRepresentation orderRepresentation, HttpRequestMessage requestMessage, HttpResponseMessage responseMessage)
        {
            var baseUri = requestMessage.RequestUri.GetLeftPart(UriPartial.Authority);
            var response = _createOrderActivity.Create(orderRepresentation, baseUri);
            responseMessage.StatusCode = HttpStatusCode.Created;
            responseMessage.Headers.Location = new Uri(response.UpdateLink);
            return response;
        }

        [WebGet(
            UriTemplate = "/{orderId}",
            RequestFormat = WebMessageFormat.Xml,
            ResponseFormat = WebMessageFormat.Xml)]
        public OrderRepresentation Get(string orderId, HttpRequestMessage requestMessage, HttpResponseMessage responseMessage)
        {
            var baseUri = requestMessage.RequestUri.GetLeftPart(UriPartial.Authority);
            var id = int.Parse(orderId);
            var response = _readOrderActivity.Read(id, baseUri);
            responseMessage.StatusCode = HttpStatusCode.OK;
            return response;
        }
    }
}