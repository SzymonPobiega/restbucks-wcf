using System;

namespace Restbucks.Service
{
    public class ResourceBinding
    {
        private readonly string _relativeUri;
        private readonly Type _resourceType;

        public ResourceBinding(string relativeUri, Type resourceType)
        {
            _relativeUri = relativeUri;
            _resourceType = resourceType;
        }

        public Type ResourceType
        {
            get { return _resourceType; }
        }

        public string RelativeUri
        {
            get { return _relativeUri; }
        }
    }
}