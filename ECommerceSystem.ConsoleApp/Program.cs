// Program.cs
using ECommerceSystem.Core.Interfaces;
using ECommerceSystem.Core.Models;
using ECommerceSystem.Core.Services;
using ECommerceSystem.Data;

namespace ECommerceConsoleApp
{
    class Program
    {
        private static IShoppingCartManager _cart;
        private static FileOrderRepository _orderRepository;
        private static List<IProduct> _availableProducts;

        static void Main(string[] args)
        {
            InitializeSystem();
            ShowMainMenu();
        }

        static void InitializeSystem()
        {
            _cart = new ShoppingCartManager();
            _orderRepository = new FileOrderRepository("orders.json");
            InitializeProducts();
        }

        static void InitializeProducts()
        {
            _availableProducts = new List<IProduct>
            {
                new PhysicalProduct("Wireless Mouse", "Ergonomic wireless mouse", 29.99m, 0.2m),
                new DigitalProduct("E-book: C# Programming", "Advanced C# programming guide", 39.99m, "https://example.com/ebook-download"),
                new PhysicalProduct("Mechanical Keyboard", "RGB gaming keyboard", 129.99m, 1.5m),
                new DigitalProduct("Online Course", "Full-stack development course", 199.99m, "https://example.com/course-access")
            };
        }

        static void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== E-Commerce System ===");
                Console.WriteLine("1. View Available Products");
                Console.WriteLine("2. View Cart");
                Console.WriteLine("3. Checkout");
                Console.WriteLine("4. Exit");
                Console.Write("Select an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        ShowProductsMenu();
                        break;
                    case "2":
                        ShowCart();
                        break;
                    case "3":
                        ProcessCheckout();
                        break;
                    case "4":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option!");
                        Thread.Sleep(1000);
                        break;
                }
            }
        }

        static void ShowProductsMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Available Products ===");
            for (int i = 0; i < _availableProducts.Count; i++)
            {
                var product = _availableProducts[i];
                Console.WriteLine($"{i + 1}. {product.Name} - {product.Price:C}");
                Console.WriteLine($"   {product.Description}");
                if (product is PhysicalProduct physical)
                    Console.WriteLine($"   Weight: {physical.Weight}kg");
                Console.WriteLine();
            }

            Console.Write("\nEnter product number to add to cart (or 0 to return): ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= _availableProducts.Count)
            {
                Console.Write("Enter quantity: ");
                if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0)
                {
                    _cart.AddProduct(_availableProducts[choice - 1], quantity);
                    Console.WriteLine("Product added to cart!");
                }
                else
                {
                    Console.WriteLine("Invalid quantity!");
                }
                Thread.Sleep(1500);
            }
        }

        static void ShowCart()
        {
            Console.Clear();
            Console.WriteLine("=== Shopping Cart ===");
            var items = _cart.GetCartItems().ToList();

            if (!items.Any())
            {
                Console.WriteLine("Your cart is empty!");
            }
            else
            {
                foreach (var item in items)
                {
                    Console.WriteLine($"{item.Product.Name} (ID: {item.Product.Id}) x{item.Quantity}");
                    Console.WriteLine($"   Price: {(item.Product.Price + item.Product.GetAdditionalCosts()):C} each");
                    Console.WriteLine($"   Total: {(item.Product.Price + item.Product.GetAdditionalCosts()) * item.Quantity:C}");
                    Console.WriteLine();
                }
                Console.WriteLine($"Total: {_cart.CalculateTotal():C}");
            }

            Console.WriteLine("\n1. Remove item");
            Console.WriteLine("2. Update quantity");
            Console.WriteLine("3. Return to main menu");
            Console.Write("Select an option: ");

            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    RemoveItemFromCart();
                    break;
                case "2":
                    UpdateItemQuantity();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid option!");
                    Thread.Sleep(1000);
                    break;
            }
        }

        static void RemoveItemFromCart()
        {
            Console.Write("Enter product ID to remove: ");
            if (Guid.TryParse(Console.ReadLine(), out Guid productId))
            {
                try
                {
                    _cart.RemoveProduct(productId);
                    Console.WriteLine("Item removed!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid product ID!");
            }
            Thread.Sleep(1500);
        }

        static void UpdateItemQuantity()
        {
            Console.Write("Enter product ID: ");
            if (Guid.TryParse(Console.ReadLine(), out Guid productId))
            {
                Console.Write("Enter new quantity: ");
                if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0)
                {
                    try
                    {
                        _cart.UpdateQuantity(productId, quantity);
                        Console.WriteLine("Quantity updated!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid quantity!");
                }
            }
            else
            {
                Console.WriteLine("Invalid product ID!");
            }
            Thread.Sleep(1500);
        }

        static void ProcessCheckout()
        {
            Console.Clear();
            Console.WriteLine("=== Checkout ===");

            if (!_cart.GetCartItems().Any())
            {
                Console.WriteLine("Your cart is empty!");
                Thread.Sleep(1500);
                return;
            }

            Console.WriteLine("Select payment method:");
            Console.WriteLine("1. Credit Card");
            Console.WriteLine("2. PayPal");
            Console.Write("Enter choice: ");

            IPaymentStrategy paymentStrategy = null;
            var choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        Console.Write("Enter card number: ");
                        var cardNumber = Console.ReadLine();
                        Console.Write("Enter expiry (MM/YY): ");
                        var expiry = Console.ReadLine();
                        Console.Write("Enter CVV: ");
                        var cvv = Console.ReadLine();
                        paymentStrategy = new CreditCardPaymentStrategy(cardNumber, expiry, cvv);
                        break;

                    case "2":
                        Console.Write("Enter PayPal email: ");
                        var email = Console.ReadLine();
                        paymentStrategy = new PayPalPaymentStrategy(email);
                        break;

                    default:
                        Console.WriteLine("Invalid payment method!");
                        Thread.Sleep(1500);
                        return;
                }

                var orderProcessor = new OrderProcessor(_cart, paymentStrategy);
                var order = orderProcessor.PlaceOrder();
                _orderRepository.SaveOrder(order);

                Console.WriteLine($"\nOrder placed successfully! Order ID: {order.OrderId}");
                Console.WriteLine($"Total: {order.TotalAmount:C}");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Checkout failed: {ex.Message}");
                Thread.Sleep(2000);
            }
        }
    }
}