using System.ServiceModel;
using System.ServiceModel.Web;

namespace Restbucks.Service.Resources
{
    [ServiceContract]
    public class PaymentResource
    {
        [WebInvoke(
            Method = "POST",
            UriTemplate = "/",
            RequestFormat = WebMessageFormat.Xml,
            ResponseFormat = WebMessageFormat.Xml)]
        public void Create()
        {
            
        }
    }
}