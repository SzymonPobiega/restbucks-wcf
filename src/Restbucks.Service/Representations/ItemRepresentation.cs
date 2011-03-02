using System.Xml.Serialization;
using Restbucks.Service.Domain;

namespace Restbucks.Service.Representations
{
    [XmlType(TypeName = "item", Namespace = RepresentationBase.RestbucksNamespace)]
    public class ItemRepresentation
    {
        [XmlElement(ElementName = "milk")]
        public Milk Milk { get; set; }

        [XmlElement(ElementName = "size")]
        public Size Size { get; set; }

        [XmlElement(ElementName = "drink")]
        public Drink Drink { get; set; }
    }
}