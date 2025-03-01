﻿namespace ECommerceSystem.Core.Exceptions
{
    public class PaymentException : Exception
    {
        public PaymentException(string message) : base(message) { }
        public PaymentException(string message, Exception inner) : base(message, inner) { }
    }
}
