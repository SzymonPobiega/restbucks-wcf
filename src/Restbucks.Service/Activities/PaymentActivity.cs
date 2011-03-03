using System;
using Restbucks.Service.Domain;
using Restbucks.Service.Mappers;
using Restbucks.Service.Representations;
using Restbucks.Service.Resources;

namespace Restbucks.Service.Activities
{
    public class PaymentActivity : IPaymentActivity
    {
        private readonly IOrderRepository _repository;
        private readonly PaymentRepresentationMapper _paymentMapper;

        public PaymentActivity(IOrderRepository repository, PaymentRepresentationMapper paymentMapper)
        {
            _repository = repository;
            _paymentMapper = paymentMapper;
        }

        public PaymentRepresentation Pay(int orderId, PaymentRepresentation paymentRepresentation, string baseUri)
        {
            var order = _repository.FindById(orderId);
            if (order == null)
            {
                throw new NoSuchOrderException(orderId);
            }
            if (order.Status != OrderStatus.Unpaid)
            {
                throw new UnexpectedOrderStateException(orderId);
            }
            
            var payment = _paymentMapper.GetDomainObject(paymentRepresentation);
            order.Pay(payment);
            var result = _paymentMapper.GetRepresentation(order.PaymentInfo);
            result.OrderLink = RestbucksResources.GetResourceUri<OrderResource>(baseUri, order.Id.ToString());
            //Add receipt link
            return result;
        }
    }
}