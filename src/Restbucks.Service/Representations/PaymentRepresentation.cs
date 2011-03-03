using System.Xml.Serialization;

namespace Restbucks.Service.Representations
{
    [XmlRoot(Namespace = RestbucksNamespace, ElementName = "payment")]
    public class PaymentRepresentation : RepresentationBase
    {
        [XmlElement(ElementName = "expiryYear")]
        public int ExpiryYear { get; set; }

        [XmlElement(ElementName = "expiryMonth")]
        public int ExpiryMonth { get; set; }

        [XmlElement(ElementName = "cardNumber")]
        public string CardNumber { get; set; }

        [XmlElement(ElementName = "cardholderName")]
        public string CardholderName { get; set; }

        [XmlElement(ElementName = "amount")]
        public decimal Amount { get; set; }

        [XmlIgnore]
        public string OrderLink
        {
            get { return GetLinkByRel(RestbucksRelation.Order).UnlessNull(x => x.Uri); }
            set { SetLink(RestbucksRelation.Order, value); }
        }

        [XmlIgnore]
        public string ReceiptLink
        {
            get { return GetLinkByRel(RestbucksRelation.Receipt).UnlessNull(x => x.Uri); }
            set { SetLink(RestbucksRelation.Receipt, value); }
        }
    }
}