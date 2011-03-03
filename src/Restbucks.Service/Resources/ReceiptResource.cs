using System;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.ServiceModel.Web;
using Restbucks.Service.Activities;
using Restbucks.Service.Domain;
using Restbucks.Service.Representations;

namespace Restbucks.Service.Resources
{
    [ServiceContract]
    public class ReceiptResource
    {
        private readonly IReadReceiptActivity _readReceiptActivity;
        private readonly ICompleteOrderActivity _completeOrderActivity;

        public ReceiptResource(IReadReceiptActivity readReceiptActivity, ICompleteOrderActivity completeOrderActivity)
        {
            _readReceiptActivity = readReceiptActivity;
            _completeOrderActivity = completeOrderActivity;
        }

        [WebGet(
            UriTemplate = "/{orderId}",
            RequestFormat = WebMessageFormat.Xml,
            ResponseFormat = WebMessageFormat.Xml)]
        public ReceiptRepresentation Get(string orderId, HttpRequestMessage requestMessage, HttpResponseMessage responseMessage)
        {
            int id;
            if (int.TryParse(orderId, out id))
            {
                try
                {
                    var baseUri = requestMessage.RequestUri.GetLeftPart(UriPartial.Authority);
                    var response = _readReceiptActivity.Read(id, baseUri);
                    return response;
                }
                catch (NoSuchOrderException)
                {
                    responseMessage.StatusCode = HttpStatusCode.NotFound;
                    return null;
                }
            }
            responseMessage.StatusCode = HttpStatusCode.BadRequest;
            return null;
        }

        [WebInvoke(
            Method = "DELETE",
            UriTemplate = "/{orderId}",
            RequestFormat = WebMessageFormat.Xml,
            ResponseFormat = WebMessageFormat.Xml)]
        public OrderRepresentation Complete(string orderId, HttpRequestMessage requestMessage, HttpResponseMessage responseMessage)
        {
            int id;
            if (int.TryParse(orderId, out id))
            {
                try
                {
                    var response = _completeOrderActivity.Complete(id);
                    return response;
                }
                catch (NoSuchOrderException)
                {
                    responseMessage.StatusCode = HttpStatusCode.NotFound;
                    return null;
                }
                catch (UnexpectedOrderStateException)
                {
                    responseMessage.StatusCode = HttpStatusCode.BadRequest;
                    return null;
                }
            }
            responseMessage.StatusCode = HttpStatusCode.BadRequest;
            return null;
        }
    }
}