using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Xml.Serialization;
using Microsoft.Net.Http;

namespace Restbucks.Client.Console
{
    public class RestbucksContentFormatter : IContentFormatter
    {
        public void WriteToStream(object instance, Stream stream)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            var serializer = new XmlSerializer(instance.GetType());
            serializer.Serialize(stream, instance);
        }

        public object ReadFromStream(Stream stream)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MediaTypeHeaderValue> SupportedMediaTypes
        {
            get { throw new NotImplementedException(); }
        }
    }
}