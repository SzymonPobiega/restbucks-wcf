using System.Collections.Generic;
using Restbucks.Service.Domain;

namespace Restbucks.Service.Tests
{
    public class InMemoryOrderRepository : IOrderRepository
    {
        private readonly Dictionary<int, Order> _orders = new Dictionary<int, Order>();
        private int _identity = 1;

        public Order FindById(int orderId)
        {
            Order order;
            _orders.TryGetValue(orderId, out order);
            return order;
        }

        public int Store(Order order)
        {
            int key = _identity;
            _identity++;
            _orders[key] = order;
            return key;
        }
    }
}