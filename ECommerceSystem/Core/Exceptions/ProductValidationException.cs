namespace ECommerceSystem.Core.Exceptions
{
    public class ProductValidationException : Exception
    {
        public ProductValidationException(string message) : base(message) { }
        public ProductValidationException(string message, Exception inner) : base(message, inner) { }
    }
}
