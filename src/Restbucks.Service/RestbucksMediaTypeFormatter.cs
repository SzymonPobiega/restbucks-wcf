using System;
using System.Net.Http.Headers;
using Microsoft.ApplicationServer.Http;
using Restbucks.Service.Representations;

namespace Restbucks.Service
{
    public class RestbucksMediaTypeFormatter : XmlMediaTypeFormatter
    {
        public RestbucksMediaTypeFormatter()
        {
            SupportedMediaTypes.Clear();
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(RepresentationBase.RestbucksMediaType));           
        }

        protected override bool OnCanReadType(Type type)
        {
            return type.Namespace == "Restbucks.Service.Representations";
        }

        protected override bool OnCanWriteType(Type type)
        {
            return type.Namespace == "Restbucks.Service.Representations";
        }
    }
}