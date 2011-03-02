using System.Xml.Serialization;

namespace Restbucks.Service.Domain
{
    public enum Drink
    {
        [XmlEnum("espresso")]
        Espresso,
        [XmlEnum("latte")]
        Latte,
        [XmlEnum("cappuccino")]
        Cappuccino,
        [XmlEnum("flatWhite")]
        FlatWhite
    }
}