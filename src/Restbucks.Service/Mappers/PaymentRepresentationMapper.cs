using Restbucks.Service.Domain;
using Restbucks.Service.Representations;

namespace Restbucks.Service.Mappers
{
    public class PaymentRepresentationMapper
    {
        public PaymentInformation GetDomainObject(PaymentRepresentation paymentRepresentation)
        {
            return new PaymentInformation(paymentRepresentation.Amount, paymentRepresentation.CardholderName,
                                          paymentRepresentation.CardNumber, paymentRepresentation.ExpiryMonth,
                                          paymentRepresentation.ExpiryYear);
        }

        public PaymentRepresentation GetRepresentation(PaymentInformation paymentInformation)
        {
            return new PaymentRepresentation()
                       {
                           Amount = paymentInformation.Amount,
                           CardholderName = paymentInformation.CardholderName,
                           CardNumber = paymentInformation.CardNumber,
                           ExpiryMonth = paymentInformation.ExpiryMonth,
                           ExpiryYear = paymentInformation.ExpiryYear
                       };
        }
    }
}