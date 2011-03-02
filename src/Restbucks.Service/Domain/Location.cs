using System.Xml.Serialization;

namespace Restbucks.Service.Domain
{
    public enum Location
    {
        [XmlEnum("takeaway")]
        Takeaway,
        [XmlEnum("inStore")]
        InStore
    }
}