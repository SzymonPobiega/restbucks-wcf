using System;
using System.Xml.Serialization;

namespace Restbucks.Service.Representations
{
    [XmlRoot(Namespace = RestbucksNamespace, ElementName = "receipt")]
    public class ReceiptRepresentation : RepresentationBase
    {
        [XmlElement(ElementName = "amount")]
        public decimal AmountPaid { get; set; }

        [XmlElement(ElementName = "paid")]
        public string PaymentDate { get; set; }

        [XmlIgnore]
        public string OrderLink
        {
            get { return GetLinkByRel(RestbucksRelation.Order).UnlessNull(x => x.Uri); }
            set { SetLink(RestbucksRelation.Order, value); }
        }
    }
}