using System;
using Restbucks.Service.Domain;
using Restbucks.Service.Mappers;
using Restbucks.Service.Representations;
using Restbucks.Service.Resources;

namespace Restbucks.Service.Activities
{
    public class ReadReceiptActivity : IReadReceiptActivity
    {
        private readonly IOrderRepository _repository;
        private readonly ReceiptRepresentationMapper _receiptMapper;

        public ReadReceiptActivity(IOrderRepository repository, ReceiptRepresentationMapper receiptMapper)
        {
            _repository = repository;
            _receiptMapper = receiptMapper;
        }

        public ReceiptRepresentation Read(int orderId, string baseUri)
        {
            var order = _repository.FindById(orderId);
            if (order == null)
            {
                throw new NoSuchOrderException(orderId);
            }
            var representation = _receiptMapper.GetRepresentation(order);
            representation.OrderLink = RestbucksResources.GetResourceUri<OrderResource>(baseUri, orderId.ToString());
            representation.CompleteLink = RestbucksResources.GetResourceUri<ReceiptResource>(baseUri, orderId.ToString());
            return representation;
        }
    }
}