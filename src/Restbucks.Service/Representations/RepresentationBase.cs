using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Restbucks.Service.Representations
{
    public abstract class RepresentationBase
    {
        public const string RestbucksNamespace = @"http://schemas.restbucks.net";
        public const string RestbucksDapNamespace = RestbucksNamespace + @"/dap";
        public const string RestbucksRelationsUri = @"http://relations.restbucks.net";
        public const string RestbucksMediaType = @"application/vnd.restbucks+xml";
        public const string SelfRelValue = @"Self";

        [XmlElement(ElementName = "link")]
        public List<Link> Links { get; set; }

        protected RepresentationBase()
        {
            Links = new List<Link>();
        }

        protected RepresentationBase(IEnumerable<Link> links)
        {
            Links = new List<Link>(links);
        }

        public Link GetLinkByRel(RestbucksRelation relSuffix)
        {
            return GetLinkByRel(relSuffix.ToRelUri());
        }

        public Link GetLinkByRel(string rel)
        {
            if (rel == null)
            {
                throw new ArgumentNullException("rel");
            }
            return Links.FirstOrDefault(x => string.CompareOrdinal(rel, x.Rel) == 0);
        }

        public void SetLink(string rel, string uri)
        {
            Links.RemoveAll(x => string.CompareOrdinal(rel, x.Rel) == 0);
            Links.Add(new Link(rel, uri));
        }

        public void SetLink(RestbucksRelation rel, string uri)
        {
            SetLink(rel.ToRelUri(), uri);
        }
    }
}