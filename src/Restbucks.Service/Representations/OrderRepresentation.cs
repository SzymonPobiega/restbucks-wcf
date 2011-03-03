using System.Collections.Generic;
using System.Xml.Serialization;
using Restbucks.Service.Domain;

namespace Restbucks.Service.Representations
{
    [XmlRoot(ElementName = "order", Namespace = RestbucksNamespace)]
    public class OrderRepresentation : RepresentationBase
    {
        [XmlElement(ElementName = "item")]
        public List<ItemRepresentation> Items { get; set; }

        [XmlElement(ElementName = "location")]
        public Location Location { get; set; }

        [XmlElement(ElementName = "cost")]
        public decimal Cost { get; set; }

        [XmlElement(ElementName = "status")]
        public OrderStatus Status { get; set; }

        [XmlIgnore]
        public string UpdateLink
        {
            get { return GetLinkByRel(RestbucksRelation.Update).UnlessNull(x => x.Uri); }
            set { SetLink(RestbucksRelation.Update, value); }
        }

        [XmlIgnore]
        public string CancelLink
        {
            get { return GetLinkByRel(RestbucksRelation.Cancel).UnlessNull(x => x.Uri); }
            set { SetLink(RestbucksRelation.Cancel, value); }
        }

        [XmlIgnore]
        public string PaymentLink
        {
            get { return GetLinkByRel(RestbucksRelation.Payment).UnlessNull(x => x.Uri); }
            set { SetLink(RestbucksRelation.Payment, value); }
        }

        [XmlIgnore]
        public string ReceiptLink
        {
            get { return GetLinkByRel(RestbucksRelation.Receipt).UnlessNull(x => x.Uri); }
            set { SetLink(RestbucksRelation.Receipt, value); }
        }

        [XmlIgnore]
        public string SelfLink
        {
            get { return GetLinkByRel(SelfRelValue).UnlessNull(x => x.Uri); }
            set { SetLink(SelfRelValue, value); }
        }
    }
}