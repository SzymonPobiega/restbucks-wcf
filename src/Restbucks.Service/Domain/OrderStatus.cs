using System.Xml.Serialization;

namespace Restbucks.Service.Domain
{
    public enum OrderStatus
    {
        [XmlEnum("unpaid")]
        Unpaid,
        [XmlEnum("preparing")]
        Praparing,
        [XmlEnum("ready")]
        Ready,
        [XmlEnum("taken")]
        Taken
    }
}