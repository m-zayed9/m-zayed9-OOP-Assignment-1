namespace OrderSystem;

public class OrderList
{
    private readonly List<Order> _orders = new();

    public IReadOnlyList<Order> All => _orders;

    public void Add(Order order)
    {
        if (FindById(order.Id) != null)
            throw new InvalidOperationException($"Order id {order.Id} already exists.");

        _orders.Add(order);
    }

    public Order? FindById(int id) => _orders.FirstOrDefault(o => o.Id == id);

    public decimal TotalPaidSales() =>
        _orders.Where(o => o.IsPaid).Sum(o => o.Total);
}
