using System;
using System.Net.Http;
using System.ServiceModel;
using Autofac;
using Microsoft.ApplicationServer.Http.Description;

namespace Restbucks.Service
{
    public class RestbucksResourceFactory : IResourceFactory
    {
        private readonly IContainer _container;

        public RestbucksResourceFactory(IContainer container)
        {
            _container = container;
        }

        public object GetInstance(Type serviceType, InstanceContext instanceContext, HttpRequestMessage request)
        {
            return _container.Resolve(serviceType);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object service)
        {
            //NO OP
        }
    }
}