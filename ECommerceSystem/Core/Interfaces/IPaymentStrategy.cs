namespace ECommerceSystem.Core.Interfaces
{
    public interface IPaymentStrategy
    {
        void ProcessPayment(decimal amount);
    }
}
