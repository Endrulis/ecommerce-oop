using ECommerceSystem.Core.Models;

namespace ECommerceSystem.Core.Interfaces
{
    public interface IShoppingCartManager
    {
        void AddProduct(IProduct product, int quantity);
        void RemoveProduct(Guid productId);
        void UpdateQuantity(Guid productId, int newQuantity);
        decimal CalculateTotal();
        IEnumerable<CartItem> GetCartItems();
        void ClearCart();
    }
}
