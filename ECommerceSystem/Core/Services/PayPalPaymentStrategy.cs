using ECommerceSystem.Core.Exceptions;
using ECommerceSystem.Core.Interfaces;

namespace ECommerceSystem.Core.Services
{
    public class PayPalPaymentStrategy : IPaymentStrategy
    {
        private readonly string _email;

        public PayPalPaymentStrategy(string email)
        {
            _email = email;
        }

        public void ProcessPayment(decimal amount)
        {
            if (amount <= 0) throw new PaymentException("Invalid payment amount");
            Console.WriteLine($"Processing PayPal payment of {amount:C} for {_email}");
        }
    }
}
