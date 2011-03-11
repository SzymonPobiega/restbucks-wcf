using System.Xml.Serialization;

namespace Restbucks.Service.Domain
{
    public enum Size
    {
        [XmlEnum("small")]
        Small,
        [XmlEnum("medium")]
        Medium,
        [XmlEnum("large")]
        Large
    }
}