using System.Collections.Generic;
using System.ServiceModel.Description;
using Microsoft.ServiceModel.Http;
using Restbucks.Service.Representations;

namespace Restbucks.Service
{
    public class RestbucksMediaTypeProcessor : XmlProcessor
    {
        public RestbucksMediaTypeProcessor(HttpOperationDescription operation, MediaTypeProcessorMode mode) : base(operation, mode)
        {
        }

        public override IEnumerable<string> SupportedMediaTypes
        {
            get { return new[] { RepresentationBase.RestbucksMediaType }; }
        }
    }
}