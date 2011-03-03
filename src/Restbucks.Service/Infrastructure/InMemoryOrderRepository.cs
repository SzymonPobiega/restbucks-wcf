using System;
using System.Collections.Generic;
using Restbucks.Service.Domain;

namespace Restbucks.Service.Infrastructure
{
    public class InMemoryOrderRepository : IOrderRepository
    {
        private const int MaxOrderCount = 1000;

        private readonly object _syncRoot = new object();
        private readonly Dictionary<int, Order> _orders = new Dictionary<int, Order>();
        private readonly List<Order> _expirationQueue = new List<Order>();
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
                _expirationQueue.Add(order);
                RemoveExpiredIfNecessary();
                return key;
            }
        }

        public void Remove(int orderId)
        {
            Order order;
            if (_orders.TryGetValue(orderId, out order))
            {
                _orders.Remove(orderId);
                _expirationQueue.Remove(order);
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
            var expired = _expirationQueue[0];
            _expirationQueue.RemoveAt(0);
            _orders.Remove(expired.Id);
        }
    }
}