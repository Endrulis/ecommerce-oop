using ECommerceSystem.Core.Exceptions;
using ECommerceSystem.Core.Interfaces;

namespace ECommerceSystem.Core.Models
{
    public class DigitalProduct : IProduct
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public decimal Price { get; }
        public string DownloadLink { get; }

        public DigitalProduct(string name, string description, decimal price, string downloadLink)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ProductValidationException("Product name cannot be empty");

            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            DownloadLink = downloadLink;
        }

        public decimal GetAdditionalCosts() => 0m;
    }
}
