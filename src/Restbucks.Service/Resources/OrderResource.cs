using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.ServiceModel.Web;
using Microsoft.ApplicationServer.Http;
using Restbucks.Service.Activities;
using Restbucks.Service.Representations;

namespace Restbucks.Service.Resources
{
    [ServiceContract]
    public class OrderResource
    {
        private readonly ICreateOrderActivity _createOrderActivity;
        private readonly IReadOrderActivity _readOrderActivity;
        private readonly IRemoveOrderActivity _removeOrderActivity;

        public OrderResource(ICreateOrderActivity createOrderActivity, IReadOrderActivity readOrderActivity, IRemoveOrderActivity removeOrderActivity)
        {
            _createOrderActivity = createOrderActivity;
            _removeOrderActivity = removeOrderActivity;
            _readOrderActivity = readOrderActivity;
        }

        [WebInvoke(
            Method = "POST", 
            UriTemplate = "/",
            RequestFormat = WebMessageFormat.Xml, 
            ResponseFormat = WebMessageFormat.Xml)]
        public HttpResponseMessage<OrderRepresentation> Create(HttpRequestMessage<OrderRepresentation> request)
        {
            var response = _createOrderActivity.Create(request.Content.ReadAs(), request.RequestUri);
            var responseMessage = new HttpResponseMessage<OrderRepresentation>(response, HttpStatusCode.Created);
            responseMessage.Headers.Location = new Uri(response.UpdateLink);
            return responseMessage;
        }

        [WebGet(
           UriTemplate = "/",
           RequestFormat = WebMessageFormat.Xml,
           ResponseFormat = WebMessageFormat.Xml)]
        public HttpResponseMessage GetClientOrderSchema()
        {
            var schemaBase = ConfigurationManager.AppSettings["schemasBaseAddress"];
            var clientOrderSchemaUri = schemaBase + "/" + "client-order.xsd";
            var responseMessage = new HttpResponseMessage(HttpStatusCode.Redirect, "");
            responseMessage.Headers.Location = new Uri(clientOrderSchemaUri);
            return responseMessage;
        }

        [WebGet(
            UriTemplate = "/{orderId}",
            RequestFormat = WebMessageFormat.Xml,
            ResponseFormat = WebMessageFormat.Xml)]
        public HttpResponseMessage<OrderRepresentation> Get(string orderId, HttpRequestMessage requestMessage)
        {
            int id;
            if (int.TryParse(orderId, out id))
            {
                try
                {
                    var response = _readOrderActivity.Read(id, requestMessage.RequestUri);
                    return new HttpResponseMessage<OrderRepresentation>(response, HttpStatusCode.OK);
                }
                catch (NoSuchOrderException)
                {
                    return new HttpResponseMessage<OrderRepresentation>(HttpStatusCode.NotFound);
                }
                
            }
            return new HttpResponseMessage<OrderRepresentation>(HttpStatusCode.BadRequest);
        }

        [WebInvoke(
            Method = "DELETE",
            UriTemplate = "/{orderId}",
            RequestFormat = WebMessageFormat.Xml,
            ResponseFormat = WebMessageFormat.Xml)]
        public HttpResponseMessage<OrderRepresentation> Cancel(string orderId, HttpRequestMessage requestMessage)
        {
            int id;
            if (int.TryParse(orderId, out id))
            {
                try
                {
                    var response = _removeOrderActivity.Remove(id);
                    return new HttpResponseMessage<OrderRepresentation>(response, HttpStatusCode.OK);
                }
                catch (NoSuchOrderException)
                {
                    return new HttpResponseMessage<OrderRepresentation>(HttpStatusCode.NotFound);
                }
                catch (UnexpectedOrderStateException)
                {
                    return new HttpResponseMessage<OrderRepresentation>(HttpStatusCode.MethodNotAllowed);
                }

            }
            return new HttpResponseMessage<OrderRepresentation>(HttpStatusCode.BadRequest);
        }
    }
}