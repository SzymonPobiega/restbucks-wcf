using System;
using System.Configuration;
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
    public class PaymentResource
    {
        private readonly IPaymentActivity _paymentActivity;

        public PaymentResource(IPaymentActivity paymentActivity)
        {
            _paymentActivity = paymentActivity;
        }

        [WebGet(
           UriTemplate = "/{paymentId}",
           RequestFormat = WebMessageFormat.Xml,
           ResponseFormat = WebMessageFormat.Xml)]
        public void GetPaymentSchema(HttpResponseMessage responseMessage)
        {
            var schemaBase = ConfigurationManager.AppSettings["schemasBaseAddress"];
            var clientOrderSchemaUri = schemaBase + "/" + "payment.xsd";
            responseMessage.Headers.Location = new Uri(clientOrderSchemaUri);
            responseMessage.StatusCode = HttpStatusCode.Redirect;
        }

        [WebInvoke(
            Method = "PUT",
            UriTemplate = "/{paymentId}",
            RequestFormat = WebMessageFormat.Xml,
            ResponseFormat = WebMessageFormat.Xml)]
        public PaymentRepresentation Pay(string paymentId, PaymentRepresentation paymentRepresentation, HttpRequestMessage requestMessage, HttpResponseMessage responseMessage)
        {
            int id;
            if (int.TryParse(paymentId, out id))
            {
                try
                {
                    var response = _paymentActivity.Pay(id, paymentRepresentation, requestMessage.RequestUri);
                    responseMessage.StatusCode = HttpStatusCode.Created;
                    responseMessage.Headers.Location = requestMessage.RequestUri;
                    return response;
                }
                catch (NoSuchOrderException)
                {
                    responseMessage.StatusCode = HttpStatusCode.NotFound;
                    return null;
                }
                catch (UnexpectedOrderStateException)
                {
                    responseMessage.StatusCode = HttpStatusCode.Forbidden;
                    return null;
                }
                catch (InvalidPaymentException)
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