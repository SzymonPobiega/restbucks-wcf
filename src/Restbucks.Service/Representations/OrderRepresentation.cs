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

        //[XmlIgnore]
        //public Link CancelLink
        //{
        //    get { return GetLinkByRel(RestbucksRelation.Cancel); }
        //}

        //[XmlIgnore]
        //public Link PaymentLink
        //{
        //    get { return GetLinkByRel(RestbucksRelation.Payment); }
        //}

        //[XmlIgnore]
        //public Link SelfLink
        //{
        //    get { return GetLinkByRel(SelfRelValue); }
        //}

        public OrderRepresentation()
        {
            
        }

        public OrderRepresentation(string orderUri, string paymentUri)
        {        
            SetLink(RestbucksRelation.Update, orderUri);
            SetLink(RestbucksRelation.Cancel, orderUri);
            SetLink(RestbucksRelation.Payment, paymentUri);
            SetLink(SelfRelValue, orderUri);
        }
    }
}