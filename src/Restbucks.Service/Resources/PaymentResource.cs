using System;
using System.Configuration;
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
        public HttpResponseMessage GetPaymentSchema()
        {
            var schemaBase = ConfigurationManager.AppSettings["schemasBaseAddress"];
            var clientOrderSchemaUri = schemaBase + "/" + "payment.xsd";
            var responseMessage = new HttpResponseMessage(HttpStatusCode.Redirect, "");
            responseMessage.Headers.Location = new Uri(clientOrderSchemaUri);
            return responseMessage;
        }

        [WebInvoke(
            Method = "PUT",
            UriTemplate = "/{paymentId}",
            RequestFormat = WebMessageFormat.Xml,
            ResponseFormat = WebMessageFormat.Xml)]
        public HttpResponseMessage<PaymentRepresentation> Pay(string paymentId, HttpRequestMessage<PaymentRepresentation> requestMessage)
        {
            int id;
            if (int.TryParse(paymentId, out id))
            {
                try
                {
                    var response = _paymentActivity.Pay(id, requestMessage.Content.ReadAs(), requestMessage.RequestUri);
                    var responseMessage = new HttpResponseMessage<PaymentRepresentation>(response, HttpStatusCode.Created);
                    responseMessage.Headers.Location = requestMessage.RequestUri;
                    return responseMessage;
                }
                catch (NoSuchOrderException)
                {
                    return new HttpResponseMessage<PaymentRepresentation>(HttpStatusCode.NotFound);
                }
                catch (UnexpectedOrderStateException)
                {
                    return new HttpResponseMessage<PaymentRepresentation>(HttpStatusCode.Forbidden);
                }
                catch (InvalidPaymentException)
                {
                    return new HttpResponseMessage<PaymentRepresentation>(HttpStatusCode.BadRequest);
                }
            }
            return new HttpResponseMessage<PaymentRepresentation>(HttpStatusCode.BadRequest);
        }
    }
}