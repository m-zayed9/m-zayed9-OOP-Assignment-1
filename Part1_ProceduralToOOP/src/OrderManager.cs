namespace OrderSystem;

public class OrderManager
{
    public CustomerList Customers { get; } = new();
    public ProductList Products { get; } = new();
    public OrderList Orders { get; } = new();

    public void AddCustomer(int id, string name, string email, string city, bool isVip) =>
        Customers.Add(new Customer(id, name, email, city, isVip));

    public void AddProduct(int id, string name, decimal price, int stock) =>
        Products.Add(new Product(id, name, price, stock));

    public Order CreateOrder(int orderId, int customerId, DateOnly date)
    {
        Customer customer = Customers.FindById(customerId)
            ?? throw new InvalidOperationException($"Customer id {customerId} not found.");

        var order = new Order(orderId, customer, date);
        Orders.Add(order);
        return order;
    }

    public void AddLineToOrder(int orderId, int productId, int quantity)
    {
        Order order = GetOrder(orderId);
        Product product = Products.FindById(productId)
            ?? throw new InvalidOperationException($"Product id {productId} not found.");

        order.AddLine(product, quantity);
    }

    public void MarkOrderPaid(int orderId) => GetOrder(orderId).MarkAsPaid();

    public Order GetOrder(int orderId) =>
        Orders.FindById(orderId)
            ?? throw new InvalidOperationException($"Order id {orderId} not found.");
}
