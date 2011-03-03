using System;

namespace Restbucks.Service.Activities
{
    public class NoSuchOrderException : Exception
    {
        private readonly int _orderId;

        public NoSuchOrderException(int orderId)
        {
            _orderId = orderId;
        }

        public int OrderId
        {
            get { return _orderId; }
        }
    }
}