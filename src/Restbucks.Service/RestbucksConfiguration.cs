using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Autofac;
using Microsoft.ServiceModel.Description;
using Microsoft.ServiceModel.Http;

namespace Restbucks.Service
{
    public class RestbucksConfiguration : HttpHostConfiguration, IProcessorProvider, IInstanceFactory
    {
        private readonly IContainer _container;

        public RestbucksConfiguration(IContainer container)
        {
            _container = container;
        }

        public void RegisterRequestProcessorsForOperation(HttpOperationDescription operation, IList<Processor> processors, MediaTypeProcessorMode mode)
        {
            processors.Add(new RestbucksMediaTypeProcessor(operation, mode));
        }

        public void RegisterResponseProcessorsForOperation(HttpOperationDescription operation, IList<Processor> processors, MediaTypeProcessorMode mode)
        {
            processors.Add(new RestbucksMediaTypeProcessor(operation, mode));
        }

        public object GetInstance(Type serviceType, InstanceContext instanceContext, Message message)
        {
            return _container.Resolve(serviceType);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object service)
        {
            // no op
        }
    }
}