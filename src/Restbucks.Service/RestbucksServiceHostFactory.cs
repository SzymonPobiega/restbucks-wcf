using System;
using System.Linq;
using System.ServiceModel;
using Microsoft.ApplicationServer.Http;
using Microsoft.ApplicationServer.Http.Activation;
using Microsoft.ApplicationServer.Http.Description;

namespace Restbucks.Service
{
    public class RestbucksServiceHostFactory : HttpConfigurableServiceHostFactory
    {
        public RestbucksServiceHostFactory(IHttpHostConfigurationBuilder configuration)
            : base(configuration)
        {
        }

        public override ServiceHostBase CreateServiceHost(string constructorString, Uri[] baseAddresses)
        {
            var host = base.CreateServiceHost(constructorString, baseAddresses);

            foreach (var httpBinding in from serviceEndpoint in host.Description.Endpoints
                                        where serviceEndpoint.ListenUri.Scheme == "https"
                                        select (HttpBinding)serviceEndpoint.Binding)
            {
                httpBinding.Security.Mode = HttpBindingSecurityMode.Transport;
            }

            return host;
        }
    }
}