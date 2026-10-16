namespace OrderSystem;

public class ConsoleMenu
{
    private readonly OrderManager _system;

    public ConsoleMenu(OrderManager system)
    {
        _system = system;
    }

    public void Run()
    {
        int choice = -1;
        while (choice != 0)
        {
            PrintMenu();
            choice = ReadInt("");

            try
            {
                HandleChoice(choice);
            }
            catch (Exception ex) when (ex is InvalidOperationException or ArgumentException)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }
    }

    private void HandleChoice(int choice)
    {
        switch (choice)
        {
            case 1: PrintCustomers(); break;
            case 2: PrintProducts(); break;
            case 3: PrintAllOrders(); break;
            case 4: PrintOrder(_system.GetOrder(ReadInt("Order id: "))); break;
            case 5: CreateOrder(); break;
            case 6: AddLine(); break;
            case 7: _system.MarkOrderPaid(ReadInt("Order id: ")); break;
            case 8: Console.WriteLine($"Paid sales total: {_system.Orders.TotalPaidSales():F2}"); break;
            case 9: AddCustomer(); break;
            case 10: AddProduct(); break;
            case 0: Console.WriteLine("Bye."); break;
            default: Console.WriteLine("Unknown choice."); break;
        }
    }

    private void AddCustomer()
    {
        int id = ReadInt("Customer id: ");
        string name = ReadText("Name: ");
        string email = ReadText("Email: ");
        string city = ReadText("City: ");
        bool isVip = ReadYesNo("VIP (y/n): ");
        _system.AddCustomer(id, name, email, city, isVip);
    }

    private void AddProduct()
    {
        int id = ReadInt("Product id: ");
        string name = ReadText("Name: ");
        decimal price = ReadDecimal("Price: ");
        int stock = ReadInt("Stock: ");
        _system.AddProduct(id, name, price, stock);
    }

    private void CreateOrder()
    {
        int orderId = ReadInt("Order id: ");
        int customerId = ReadInt("Customer id: ");
        DateOnly date = ReadDate("Date (YYYY-MM-DD): ");
        _system.CreateOrder(orderId, customerId, date);
    }

    private void AddLine()
    {
        int orderId = ReadInt("Order id: ");
        int productId = ReadInt("Product id: ");
        int quantity = ReadInt("Quantity: ");
        _system.AddLineToOrder(orderId, productId, quantity);
    }

    // ---------- printing ----------

    public void PrintCustomers()
    {
        Console.WriteLine($"\n=== CUSTOMERS ({_system.Customers.All.Count}) ===");
        foreach (Customer customer in _system.Customers.All)
            Console.WriteLine(customer);
    }

    public void PrintProducts()
    {
        Console.WriteLine($"\n=== PRODUCTS ({_system.Products.All.Count}) ===");
        foreach (Product product in _system.Products.All)
            Console.WriteLine(product);
    }

    public void PrintAllOrders()
    {
        Console.WriteLine($"\n=== ALL ORDERS ({_system.Orders.All.Count}) ===");
        foreach (Order order in _system.Orders.All)
            PrintOrder(order);
    }

    public void PrintOrder(Order order)
    {
        Console.WriteLine($"\n=== ORDER #{order.Id} ===");
        Console.WriteLine($"Date: {order.Date:yyyy-MM-dd}");
        Console.WriteLine($"Customer: {order.Customer.Name} (#{order.Customer.Id})");
        Console.WriteLine($"Paid: {(order.IsPaid ? "yes" : "no")}");
        Console.WriteLine("Lines:");

        foreach (OrderLine line in order.Lines)
        {
            Console.WriteLine(
                $"  - {line.Product.Name}  x{line.Quantity}  @{line.UnitPrice:F2}  = {line.LineTotal:F2}");
        }

        Console.WriteLine($"TOTAL: {order.Total:F2}");
    }

    private static void PrintMenu()
    {
        Console.WriteLine("\n---------- MENU ----------");
        Console.WriteLine("1) Print customers");
        Console.WriteLine("2) Print products");
        Console.WriteLine("3) Print all orders");
        Console.WriteLine("4) Print one order by id");
        Console.WriteLine("5) Create order");
        Console.WriteLine("6) Add line to order");
        Console.WriteLine("7) Mark order paid");
        Console.WriteLine("8) Show paid sales total");
        Console.WriteLine("9) Add customer");
        Console.WriteLine("10) Add product");
        Console.WriteLine("0) Exit");
        Console.Write("Choice: ");
    }

    // ---------- safe input helpers ----------

    private static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int value))
                return value;

            Console.WriteLine("Please enter a valid number.");
        }
    }

    private static decimal ReadDecimal(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (decimal.TryParse(Console.ReadLine(), out decimal value))
                return value;

            Console.WriteLine("Please enter a valid number.");
        }
    }

    private static string ReadText(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? text = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(text))
                return text.Trim();

            Console.WriteLine("This field cannot be empty.");
        }
    }

    private static bool ReadYesNo(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? answer = Console.ReadLine()?.Trim().ToLower();
            if (answer == "y" || answer == "yes") return true;
            if (answer == "n" || answer == "no") return false;

            Console.WriteLine("Please answer y or n.");
        }
    }

    private static DateOnly ReadDate(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (DateOnly.TryParse(Console.ReadLine(), out DateOnly value))
                return value;

            Console.WriteLine("Please enter a valid date.");
        }
    }
}
