using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Routing;
using Microsoft.ServiceModel.Http;
using Restbucks.Service.Resources;

namespace Restbucks.Service
{
    public static class RestbucksResources
    {
        private static readonly List<ResourceBinding> _resources = new List<ResourceBinding>();
        private static readonly string _baseAddress;

        private static void Bind<T>(string relativeUri)
        {
            _resources.Add(new ResourceBinding(relativeUri, typeof(T)));
        }

        static RestbucksResources()
        {
            Bind<OrderResource>("order");
            Bind<PaymentResource>("payment");
            Bind<ReceiptResource>("receipt");
            _baseAddress = ConfigurationManager.AppSettings["baseAddress"];
        }

        public static void RegisterRoutes(HttpHostConfiguration configuration)
        {
            foreach (var resourceBinding in _resources)
            {
                AddServiceRoute(resourceBinding.ResourceType, resourceBinding.RelativeUri, configuration);
            }
        }

        public static string GetResourceUri<T>(Uri requestUri, string suffix)
        {
            var result = _baseAddress + "/" + GetResourceUri<T>() + "/" + suffix;
            return result;
        }

        public static string GetResourceUri<T>()
        {
            var binding = _resources.FirstOrDefault(x => x.ResourceType == typeof (T));
            if (binding == null)
            {
                throw new InvalidOperationException(string.Format("Resource {0} is not registered.", typeof(T).FullName));
            }
            return binding.RelativeUri;
        }

        private static void AddServiceRoute(Type serviceType, string routePrefix, HttpHostConfiguration configuration)
        {            
            var route = new ServiceRoute(routePrefix, new WebHttpServiceHostFactory {Configuration = configuration}, serviceType);
            RouteTable.Routes.Add(route);
        }
    }
}