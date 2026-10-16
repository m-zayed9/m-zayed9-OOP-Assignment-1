using System.Globalization;

namespace OrderSystem;

public class Program
{
    public static void Main()
    {
        var manager = new OrderManager();
        var menu = new ConsoleMenu(manager);

        SeedSampleData(manager);
        RunDemoScenario(manager);

        menu.PrintCustomers();
        menu.PrintProducts();
        menu.PrintAllOrders();
        Console.WriteLine($"\nPaid sales total after demo: {manager.Orders.TotalPaidSales():F2}");

        menu.Run();
    }

    private static void SeedSampleData(OrderManager manager)
    {
        manager.AddCustomer(1, "Mona Ali", "mona@example.com", "Cairo", true);
        manager.AddCustomer(2, "Omar Hassan", "omar@example.com", "Alexandria", false);
        manager.AddCustomer(3, "Sara Nabil", "sara@example.com", "Giza", false);

        manager.AddProduct(101, "USB Cable", 50m, 100);
        manager.AddProduct(102, "Wireless Mouse", 250m, 40);
        manager.AddProduct(103, "Mechanical Keyboard", 1200m, 15);
        manager.AddProduct(104, "Laptop Stand", 400m, 25);
    }

    private static void RunDemoScenario(OrderManager manager)
    {
        manager.CreateOrder(1001, 1, new DateOnly(2026, 9, 15));
        manager.AddLineToOrder(1001, 101, 2);
        manager.AddLineToOrder(1001, 102, 1);
        manager.MarkOrderPaid(1001);

        manager.CreateOrder(1002, 2, new DateOnly(2026, 9, 15));
        manager.AddLineToOrder(1002, 103, 1);
        manager.AddLineToOrder(1002, 104, 1);

        manager.CreateOrder(1003, 3, new DateOnly(2026, 9, 16));
        manager.AddLineToOrder(1003, 101, 5);
        manager.MarkOrderPaid(1003);
    }
}
