using System;
using System.Collections.Generic;
using System.Linq;

namespace Restbucks.Service.Domain
{
    public class Order
    {
        public int Id { get; internal set; }

        private readonly List<Item> _itemsCollection;
        public IEnumerable<Item> Items
        {
            get { return _itemsCollection; }
        }

        public PaymentInformation PaymentInfo { get; private set; }
        public Location Location { get; private set; }
        public OrderStatus Status { get; private set; }
        public DateTime PaymentDateUtc { get; private set; }

        public Order(Location location, IEnumerable<Item> items)
        {
            Location = location;
            _itemsCollection = new List<Item>(items);
        }

        public void Pay(PaymentInformation paymentInformation)
        {
            if (CalculateTotal() != paymentInformation.Amount)
            {
                throw new InvalidPaymentException();
            }
            Status = OrderStatus.Preparing;
            PaymentDateUtc = DateTime.UtcNow;
            PaymentInfo = paymentInformation;
        }

        public void Prepare()
        {
            Status = OrderStatus.Ready;
        }
        public void Take()
        {
            Status = OrderStatus.Taken;
        }

        public decimal CalculateTotal()
        {
            return _itemsCollection.Sum(x => x.CalculateCost());
        }
    }
}