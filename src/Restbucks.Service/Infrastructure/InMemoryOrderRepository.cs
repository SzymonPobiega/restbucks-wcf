using System.Collections.Generic;
using Restbucks.Service.Domain;

namespace Restbucks.Service.Infrastructure
{
    public class InMemoryOrderRepository : IOrderRepository
    {
        private const int MaxOrderCount = 1000;

        private readonly object _syncRoot = new object();
        private readonly Dictionary<int, Order> _orders = new Dictionary<int, Order>();
        private readonly Queue<Order> _expirationQueue = new Queue<Order>();
        private int _identity = 1;

        public Order FindById(int orderId)
        {
            lock (_syncRoot)
            {
                Order order;
                _orders.TryGetValue(orderId, out order);
                return order;
            }
        }

        public int Store(Order order)
        {
            lock (_syncRoot)
            {
                int key = _identity;
                _identity++;
                order.Id = key;
                _orders[key] = order;
                _expirationQueue.Enqueue(order);
                RemoveExpiredIfNecessary();
                return key;
            }
        }

        private void RemoveExpiredIfNecessary()
        {
            if (_orders.Count <= MaxOrderCount)
            {
                return;                
            }
            RemoveExpired();
        }

        private void RemoveExpired()
        {
            var expired = _expirationQueue.Dequeue();
            _orders.Remove(expired.Id);
        }
    }
}