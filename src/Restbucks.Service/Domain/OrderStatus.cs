using System.Xml.Serialization;

namespace Restbucks.Service.Domain
{
    public enum OrderStatus
    {
        [XmlEnum("unpaid")]
        Unpaid,
        [XmlEnum("preparing")]
        Preparing,
        [XmlEnum("ready")]
        Ready,
        [XmlEnum("taken")]
        Taken
    }
}