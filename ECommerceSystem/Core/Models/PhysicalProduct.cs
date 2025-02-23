using ECommerceSystem.Core.Exceptions;
using ECommerceSystem.Core.Interfaces;

namespace ECommerceSystem.Core.Models
{
    public class PhysicalProduct : IProduct
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public decimal Price { get; }
        public decimal Weight { get; }

        public PhysicalProduct(string name, string description, decimal price, decimal weight)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ProductValidationException("Product name cannot be empty");

            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            Weight = weight;
        }

        public decimal GetAdditionalCosts() => Weight * 0.5m; // $0.5 per kg shipping
    }
}
