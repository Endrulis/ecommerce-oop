using ECommerceSystem.Core.Exceptions;
using ECommerceSystem.Core.Interfaces;

namespace ECommerceSystem.Core.Services
{
    public class CreditCardPaymentStrategy : IPaymentStrategy
    {
        private readonly string _cardNumber;
        private readonly string _expiryDate;
        private readonly string _cvv;

        public CreditCardPaymentStrategy(string cardNumber, string expiryDate, string cvv)
        {
            _cardNumber = cardNumber;
            _expiryDate = expiryDate;
            _cvv = cvv;
        }

        public void ProcessPayment(decimal amount)
        {
            // Simulate payment processing
            if (amount <= 0) throw new PaymentException("Invalid payment amount");
            Console.WriteLine($"Processing credit card payment of {amount:C}");
        }
    }
}
