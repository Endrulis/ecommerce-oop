using ECommerceSystem.Core.Models;
using Newtonsoft.Json;

namespace ECommerceSystem.Data
{
    public class FileOrderRepository
    {
        private readonly string _filePath;

        public FileOrderRepository(string filePath)
        {
            _filePath = filePath;
            EnsureFileExists();
        }

        public void SaveOrder(Order order)
        {
            var orders = GetAllOrders().ToList();
            orders.Add(order);
            SaveAllOrders(orders);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            if (!File.Exists(_filePath)) return new List<Order>();

            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<Order>>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                Error = (sender, args) => args.ErrorContext.Handled = true
            }) ?? new List<Order>();
        }

        private void SaveAllOrders(IEnumerable<Order> orders)
        {
            var json = JsonConvert.SerializeObject(orders, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
            File.WriteAllText(_filePath, json);
        }

        private void EnsureFileExists()
        {
            if (!File.Exists(_filePath))
                File.WriteAllText(_filePath, "[]");
        }
    }
}
