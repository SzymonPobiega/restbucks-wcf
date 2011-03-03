using Restbucks.Service.Domain;
using Restbucks.Service.Representations;

namespace Restbucks.Service.Mappers
{
    public class ReceiptRepresentationMapper
    {
        public ReceiptRepresentation GetRepresentation(Order order)
        {
            return new ReceiptRepresentation
                       {
                           AmountPaid = order.PaymentInfo.Amount,
                           PaymentDate = order.PaymentDateUtc.ToLongDateString()
                       };
        }
    }
}