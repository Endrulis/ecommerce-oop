namespace ECommerceSystem.Core.Models
{
    public class Order
    {
        public Guid OrderId { get; }
        public DateTime OrderDate { get; }
        public decimal TotalAmount { get; }
        public List<CartItem> Items { get; }

        public Order(decimal totalAmount, List<CartItem> items)
        {
            OrderId = Guid.NewGuid();
            OrderDate = DateTime.UtcNow;
            TotalAmount = totalAmount;
            Items = items;
        }
    }
}
