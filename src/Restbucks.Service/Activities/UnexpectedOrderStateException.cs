using System;

namespace Restbucks.Service.Activities
{
    public class UnexpectedOrderStateException : Exception
    {
        private readonly int _orderId;

        public UnexpectedOrderStateException(int orderId)
        {
            _orderId = orderId;
        }

        public int OrderId
        {
            get { return _orderId; }
        }
    }
}