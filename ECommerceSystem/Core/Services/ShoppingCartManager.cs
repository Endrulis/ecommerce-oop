using ECommerceSystem.Core.Exceptions;
using ECommerceSystem.Core.Interfaces;
using ECommerceSystem.Core.Models;

namespace ECommerceSystem.Core.Services
{
    public class ShoppingCartManager : IShoppingCartManager
    {
        private readonly Dictionary<Guid, CartItem> _cartItems = new();

        public void AddProduct(IProduct product, int quantity)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            if (quantity <= 0) throw new ArgumentException("Quantity must be positive");

            if (_cartItems.TryGetValue(product.Id, out var existingItem))
                existingItem.UpdateQuantity(existingItem.Quantity + quantity);
            else
                _cartItems[product.Id] = new CartItem(product, quantity);
        }

        public void RemoveProduct(Guid productId)
        {
            if (!_cartItems.ContainsKey(productId))
                throw new ProductValidationException($"Product {productId} not in cart");

            _cartItems.Remove(productId);
        }

        public void UpdateQuantity(Guid productId, int newQuantity)
        {
            if (!_cartItems.TryGetValue(productId, out var item))
                throw new ProductValidationException($"Product {productId} not in cart");

            item.UpdateQuantity(newQuantity);
        }

        public decimal CalculateTotal() =>
            _cartItems.Values.Sum(item => (item.Product.Price + item.Product.GetAdditionalCosts()) * item.Quantity);

        public IEnumerable<CartItem> GetCartItems() => _cartItems.Values;
        public void ClearCart() => _cartItems.Clear();
    }
}
