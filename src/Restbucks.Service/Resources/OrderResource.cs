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

        public OrderResource(ICreateOrderActivity createOrderActivity)
        {
            _createOrderActivity = createOrderActivity;
        }

        [WebInvoke(Method = "POST", UriTemplate = "/",
            RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml)]
        public OrderRepresentation Create(OrderRepresentation orderRepresentation, HttpRequestMessage requestMessage, HttpResponseMessage responseMessage)
        {
            var baseUri = requestMessage.RequestUri.GetLeftPart(UriPartial.Authority);
            var response = _createOrderActivity.Create(orderRepresentation, baseUri);
            responseMessage.StatusCode = HttpStatusCode.Created;
            responseMessage.Headers.Location = new Uri(orderRepresentation.UpdateLink);
            return response;
        }
    }
}