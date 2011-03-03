using System;
using Restbucks.Service.Domain;
using Restbucks.Service.Mappers;
using Restbucks.Service.Representations;

namespace Restbucks.Service.Activities
{
    public class CompleteOrderActivity : ICompleteOrderActivity
    {
        private readonly IOrderRepository _repository;
        private readonly OrderRepresentationMapper _orderRepresentationMapper;

        public CompleteOrderActivity(IOrderRepository repository, OrderRepresentationMapper orderRepresentationMapper)
        {
            _repository = repository;
            _orderRepresentationMapper = orderRepresentationMapper;
        }

        public OrderRepresentation Complete(int orderId)
        {
            var order = _repository.FindById(orderId);
            if (order == null)
            {
                throw new NoSuchOrderException(orderId);
            }

            if (order.Status != OrderStatus.Ready)
            {
                throw new UnexpectedOrderStateException(orderId);
            }
            order.Take();

            var representation = _orderRepresentationMapper.GetRepresentation(order);
            return representation;
        }
    }
}