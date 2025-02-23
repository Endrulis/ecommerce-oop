using ECommerceSystem.Core.Exceptions;
using ECommerceSystem.Core.Interfaces;
using ECommerceSystem.Core.Models;

namespace ECommerceSystem.Core.Services
{
    public class OrderProcessor
    {
        private readonly IShoppingCartManager _cart;
        private IPaymentStrategy _paymentStrategy;

        public OrderProcessor(IShoppingCartManager cart, IPaymentStrategy paymentStrategy)
        {
            _cart = cart ?? throw new ArgumentNullException(nameof(cart));
            _paymentStrategy = paymentStrategy ?? throw new ArgumentNullException(nameof(paymentStrategy));
        }

        public Order PlaceOrder()
        {
            var total = _cart.CalculateTotal();
            if (total <= 0) throw new OrderException("Cannot place order with zero total");

            try
            {
                _paymentStrategy.ProcessPayment(total);
                var order = new Order(total, _cart.GetCartItems().ToList());
                _cart.ClearCart();
                return order;
            }
            catch (PaymentException ex)
            {
                throw new OrderException("Payment processing failed", ex);
            }
        }

        public void SetPaymentStrategy(IPaymentStrategy paymentStrategy) =>
            _paymentStrategy = paymentStrategy ?? throw new ArgumentNullException(nameof(paymentStrategy));
    }
}
