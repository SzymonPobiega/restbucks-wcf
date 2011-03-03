namespace Restbucks.Service.Domain
{
    public class PaymentInformation
    {
        private readonly decimal _amount;
        private readonly string _cardholderName;
        private readonly string _cardNumber;
        private readonly int _expiryMonth;
        private readonly int _expiryYear;

        public PaymentInformation(decimal amount, string cardholderName, string cardNumber, int expiryMonth, int expiryYear)
        {
            _amount = amount;
            _expiryYear = expiryYear;
            _expiryMonth = expiryMonth;
            _cardNumber = cardNumber;
            _cardholderName = cardholderName;
        }

        public int ExpiryYear
        {
            get { return _expiryYear; }
        }

        public int ExpiryMonth
        {
            get { return _expiryMonth; }
        }

        public string CardNumber
        {
            get { return _cardNumber; }
        }

        public string CardholderName
        {
            get { return _cardholderName; }
        }

        public decimal Amount
        {
            get { return _amount; }
        }
    }
}