using System.Collections.Generic;
using System.Linq;

namespace Restbucks.Service.Domain
{
    public class Order
    {
        public virtual int Id { get; internal set; }

        protected virtual List<Item> ItemsCollection { get; set; }
        public virtual IEnumerable<Item> Items
        {
            get { return ItemsCollection; }
        }

        public virtual Location Location { get; protected set; }        

        public virtual OrderStatus Status { get; protected set; }

        protected Order()
        { 
        }

        public void Pay()
        {
            Status = OrderStatus.Preparing;
        }

        public Order(Location location, IEnumerable<Item> items)
        {
            Location = location;
            ItemsCollection = new List<Item>(items);
        }

        public decimal CalculateTotal()
        {
            return ItemsCollection.Sum(x => x.CalculateCost());
        }
    }
}