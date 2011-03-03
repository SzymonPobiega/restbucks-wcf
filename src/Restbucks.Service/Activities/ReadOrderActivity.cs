using System;
using Restbucks.Service.Domain;
using Restbucks.Service.Mappers;
using Restbucks.Service.Representations;
using Restbucks.Service.Resources;

namespace Restbucks.Service.Activities
{
    public class ReadOrderActivity : IReadOrderActivity
    {
        private readonly IOrderRepository _repository;
        private readonly OrderRepresentationMapper _orderRepresentationMapper;

        public ReadOrderActivity(IOrderRepository repository, OrderRepresentationMapper orderRepresentationMapper)
        {
            _repository = repository;
            _orderRepresentationMapper = orderRepresentationMapper;
        }

        public OrderRepresentation Read(int orderId, string baseUri)
        {
            var order = _repository.FindById(orderId);
            if (order == null)
            {
                throw new NoSuchOrderException(orderId);
            }

            var representation = _orderRepresentationMapper.GetRepresentation(order);
            var orderUri = RestbucksResources.GetResourceUri<OrderResource>(baseUri, orderId.ToString());

            if (order.Status == OrderStatus.Unpaid)
            {
                var paymentUri = RestbucksResources.GetResourceUri<PaymentResource>(baseUri, orderId.ToString());
                representation.PaymentLink = paymentUri;
                representation.CancelLink = orderUri;
                representation.UpdateLink = orderUri;
                representation.SelfLink = orderUri;
            }
            else if (order.Status == OrderStatus.Preparing)
            {
                representation.SelfLink = orderUri;
            }
            else if (order.Status == OrderStatus.Ready)
            {
                representation.ReceiptLink = RestbucksResources.GetResourceUri<ReceiptResource>(baseUri,orderId.ToString());
            }
            return representation;
        }
    }
}