namespace ECommerceSystem.Core.Interfaces
{
    public interface IProduct
    {
        Guid Id { get; }
        string Name { get; }
        string Description { get; }
        decimal Price { get; }
        decimal GetAdditionalCosts();
    }
}
