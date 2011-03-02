using System.Xml.Serialization;

namespace Restbucks.Service.Representations
{
    [XmlRoot(ElementName = "link", Namespace = RepresentationBase.RestbucksDapNamespace)]
    public class Link
    {
        [XmlAttribute(AttributeName = "rel")]
        public string Rel { get; set; }
        [XmlAttribute(AttributeName = "uri")]
        public string Uri { get; set; }
        [XmlAttribute(AttributeName = "mediaType")]
        public string MediaType { get; set; }

        public Link()
        {            
        }

        public Link(RestbucksRelation rel, string uri)
            : this(rel.ToRelUri(), uri)
        {
        }

        public Link(string rel, string uri)
            : this(rel, uri, RepresentationBase.RestbucksMediaType)
        {            
        }

        public Link(string rel, string uri, string mediaType)
        {
            Rel = rel;
            Uri = uri;
            MediaType = mediaType;
        }
    }
}