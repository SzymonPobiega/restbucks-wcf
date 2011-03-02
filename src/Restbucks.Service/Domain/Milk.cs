using System.Xml.Serialization;

namespace Restbucks.Service.Domain
{
    public enum Milk
    {
        [XmlEnum("whole")]
        Whole,
        [XmlEnum("skim")]
        Skim,
        [XmlEnum("semi")]
        Semi,
        [XmlEnum("none")]
        None
    }
}