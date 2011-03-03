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

        public ReceiptResource(IReadReceiptActivity readReceiptActivity)
        {
            _readReceiptActivity = readReceiptActivity;
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
    }
}