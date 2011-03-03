using Restbucks.Service.Domain;
using Restbucks.Service.Mappers;
using Restbucks.Service.Representations;

namespace Restbucks.Service.Activities
{
    public class RemoveOrderActivity : IRemoveOrderActivity
    {
        private readonly IOrderRepository _repository;
        private readonly OrderRepresentationMapper _orderRepresentationMapper;

        public RemoveOrderActivity(IOrderRepository repository, OrderRepresentationMapper orderRepresentationMapper)
        {
            _repository = repository;
            _orderRepresentationMapper = orderRepresentationMapper;
        }

        public OrderRepresentation Remove(int orderId)
        {
            var order = _repository.FindById(orderId);

            if (order == null)
            {
                throw new NoSuchOrderException(orderId);
            }

            if (order.Status == OrderStatus.Preparing || order.Status == OrderStatus.Ready)
            {
                throw new UnexpectedOrderStateException(orderId);
            }

            var representation = _orderRepresentationMapper.GetRepresentation(order);
            _repository.Remove(orderId);
            return representation;
        }
    }
}