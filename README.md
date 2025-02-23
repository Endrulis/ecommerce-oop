# E-Commerce System with OOP Principles

![E-Commerce System](https://img.shields.io/badge/Status-Completed-brightgreen)
![C#](https://img.shields.io/badge/Language-C%23-blue)
![.NET](https://img.shields.io/badge/Framework-.NET%208.0-purple)

A fully functional e-commerce system built using **Object-Oriented Programming (OOP)** principles and best practices. This project demonstrates clean architecture, SOLID principles, and design patterns in a real-world application.

---

## Features

- **Product Management**:
  - Supports physical and digital products.
  - Each product type has unique attributes (e.g., weight for physical products, download link for digital products).
  
- **Shopping Cart**:
  - Add, remove, and update products.
  - Quantity management for each product.
  - Automatic calculation of total costs, including additional fees (e.g., shipping for physical products).

- **Order Processing**:
  - Checkout with multiple payment methods (Credit Card, PayPal).
  - Uses the **Strategy Pattern** for flexible payment processing.
  - Order confirmation with unique order ID.

- **Data Persistence**:
  - Save and load orders using JSON file storage.
  - Orders are stored in `orders.json` for easy retrieval.

- **Error Handling**:
  - Custom exceptions for product validation, payment processing, and order management.
  - Input validation for user-friendly error messages.

---

## Technologies Used

- **C#** (.NET 8.0)
- **OOP Principles**:
  - Encapsulation
  - Inheritance
  - Polymorphism
  - Abstraction
- **Design Patterns**:
  - Strategy Pattern (Payment Methods)
  - Repository Pattern (Order Storage)
- **File I/O**: JSON serialization for data persistence.

---

## How to Run

### 1. Clone the Repository
```bash
git clone https://github.com/Endrulis/ecommerce-oop.git
cd ecommerce-oop
```

### 2. Open in Visual Studio
- Open the solution file `ECommerceSystem.sln` in Visual Studio.
- Set the `ECommerceConsoleApp` project as the startup project.

### 3. Run the Application
- Build and run the project.
- Follow the console prompts to:
  - Browse products
  - Manage your shopping cart
  - Checkout using your preferred payment method

---

## Project Structure
```
ecommerce-oop/
├── ECommerceSystem.Core/          # Core class library
│   ├── Interfaces/                # Interfaces for products, cart, and payments
│   ├── Models/                    # Product and cart item models
│   ├── Services/                  # Shopping cart and order processing logic
│   ├── Exceptions/                # Custom exceptions
├── ECommerceConsoleApp/           # Console application
│   └── Program.cs                 # Main application logic
├── ECommerceSystem.Data/          # Data persistence layer
│   └── FileOrderRepository.cs     # JSON-based order storage
├── README.md                      # Project documentation
└── ECommerceSystem.sln            # Visual Studio solution file
```

---

## Key OOP Concepts Demonstrated

### **Encapsulation**
- Private fields with public properties.
- Methods for modifying internal state (e.g., `UpdateQuantity`).

### **Inheritance**
- `PhysicalProduct` and `DigitalProduct` inherit from a base product interface.

### **Polymorphism**
- Different product types and payment methods are handled through interfaces.

### **Abstraction**
- High-level interfaces (`IProduct`, `IPaymentStrategy`) hide implementation details.

### **SOLID Principles**
- **Single Responsibility**: Each class has a single purpose.
- **Open/Closed**: Extendable payment methods and product types.
- **Liskov Substitution**: Derived classes can replace base classes.
- **Interface Segregation**: Small, focused interfaces.
- **Dependency Inversion**: High-level modules depend on abstractions.

---

## Design Patterns Used

### **Strategy Pattern**
- Used for payment processing.
- Different payment methods (Credit Card, PayPal) implement the same interface (`IPaymentStrategy`).

### **Repository Pattern**
- Used for order storage.
- The `FileOrderRepository` handles saving and loading orders to/from a JSON file.

---

## Example Workflow

### **Browse Products**
- View available products and add them to your cart.

### **Manage Cart**
- Update quantities or remove items using product IDs.

### **Checkout**
- Choose a payment method (Credit Card or PayPal).
- Enter payment details.
- Confirm your order.

### **View Orders**
- Orders are saved to `orders.json` for future reference.

---

## Future Enhancements

- Add inventory management.
- Implement discount codes and promotions.
- Add shipping address collection.
- Support more payment methods (e.g., Apple Pay, Google Pay).
- Add unit tests for core functionality.