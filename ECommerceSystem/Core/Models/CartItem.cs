using ECommerceSystem.Core.Interfaces;

namespace ECommerceSystem.Core.Models
{
    public class CartItem
    {
        public IProduct Product { get; }
        public int Quantity { get; private set; }

        public CartItem(IProduct product, int quantity)
        {
            Product = product ?? throw new ArgumentNullException(nameof(product));
            Quantity = quantity > 0 ? quantity : throw new ArgumentException("Quantity must be positive");
        }

        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity <= 0)
                throw new ArgumentException("Quantity must be positive");
            Quantity = newQuantity;
        }
    }
}
