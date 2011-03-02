using System;
using Restbucks.Service.Domain;
using Restbucks.Service.Mappers;
using Restbucks.Service.Representations;

namespace Restbucks.Service.Activities
{
    public class CreateOrderActivity : ICreateOrderActivity
    {
        private readonly IOrderRepository _repository;
        private readonly OrderRepresentationMapper _orderRepresentationMapper;

        public CreateOrderActivity(IOrderRepository repository, OrderRepresentationMapper orderRepresentationMapper)
        {
            _repository = repository;
            _orderRepresentationMapper = orderRepresentationMapper;
        }

        public OrderRepresentation Create(OrderRepresentation orderRepresentation, string baseUri)
        {
            var newOrder = _orderRepresentationMapper.GetDomainObject(orderRepresentation);
            var orderId = _repository.Store(newOrder);

            var orderUri = baseUri + "/order/" + orderId;
            var paymentUri = baseUri + "/payment/" + orderId;

            var result = _orderRepresentationMapper.GetRepresentation(newOrder);
            result.UpdateLink = orderUri;
            result.SelfLink = orderUri;
            result.CancelLink = orderUri;
            result.PaymentLink = paymentUri;
            return result;
        }
    }
}