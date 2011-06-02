using System;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.ServiceModel.Web;
using Microsoft.ApplicationServer.Http;
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
        public HttpResponseMessage<ReceiptRepresentation> Get(string orderId, HttpRequestMessage requestMessage)
        {
            int id;
            if (int.TryParse(orderId, out id))
            {
                try
                {
                    var response = _readReceiptActivity.Read(id, requestMessage.RequestUri);
                    return new HttpResponseMessage<ReceiptRepresentation>(response, HttpStatusCode.OK);
                }
                catch (NoSuchOrderException)
                {
                    return new HttpResponseMessage<ReceiptRepresentation>(HttpStatusCode.NotFound);                    
                }
            }
            return new HttpResponseMessage<ReceiptRepresentation>(HttpStatusCode.BadRequest);            
        }

        [WebInvoke(
            Method = "DELETE",
            UriTemplate = "/{orderId}",
            RequestFormat = WebMessageFormat.Xml,
            ResponseFormat = WebMessageFormat.Xml)]
        public HttpResponseMessage<OrderRepresentation> Complete(string orderId, HttpRequestMessage requestMessage)
        {
            int id;
            if (int.TryParse(orderId, out id))
            {
                try
                {
                    var response = _completeOrderActivity.Complete(id);
                    return new HttpResponseMessage<OrderRepresentation>(response, HttpStatusCode.OK);
                }
                catch (NoSuchOrderException)
                {
                    return new HttpResponseMessage<OrderRepresentation>(HttpStatusCode.NotFound);                    
                }
                catch (UnexpectedOrderStateException)
                {
                    return new HttpResponseMessage<OrderRepresentation>(HttpStatusCode.BadRequest);                    
                }
            }
            return new HttpResponseMessage<OrderRepresentation>(HttpStatusCode.BadRequest);            
        }
    }
}