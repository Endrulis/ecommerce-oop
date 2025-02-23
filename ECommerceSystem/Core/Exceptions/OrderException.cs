namespace ECommerceSystem.Core.Exceptions
{
    public class OrderException : Exception
    {
        public OrderException(string message) : base(message) { }
        public OrderException(string message, Exception inner) : base(message, inner) { }
    }
}
