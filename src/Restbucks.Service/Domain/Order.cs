using System.Collections.Generic;

namespace Restbucks.Service.Domain
{
    public class Order
    {
        protected virtual List<Item> ItemsCollection { get; set; }
        public virtual IEnumerable<Item> Items
        {
            get { return ItemsCollection; }
        }

        public virtual Location Location { get; protected set; }

        public virtual decimal Total { get; protected set; }

        public virtual OrderStatus Status { get; protected set; }

        protected Order()
        { 
        }

        protected Order(Location location, IEnumerable<Item> items)
        {
            Location = location;
            ItemsCollection = new List<Item>(items);
        }
    }
}