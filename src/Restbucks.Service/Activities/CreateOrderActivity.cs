using System;
using Restbucks.Service.Domain;
using Restbucks.Service.Mappers;
using Restbucks.Service.Representations;
using Restbucks.Service.Resources;

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

        public OrderRepresentation Create(OrderRepresentation orderRepresentation, Uri requestUri)
        {
            var newOrder = _orderRepresentationMapper.GetDomainObject(orderRepresentation);
            var orderId = _repository.Store(newOrder);

            var orderUri = RestbucksResources.GetResourceUri<OrderResource>(requestUri, orderId.ToString());
            var paymentUri = RestbucksResources.GetResourceUri<PaymentResource>(requestUri, orderId.ToString());

            var result = _orderRepresentationMapper.GetRepresentation(newOrder);
            result.UpdateLink = orderUri;
            result.SelfLink = orderUri;
            result.CancelLink = orderUri;
            result.PaymentLink = paymentUri;
            return result;
        }
    }
}