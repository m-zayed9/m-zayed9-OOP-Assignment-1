namespace OrderSystem;

public class Order
{
    private const int MaxLines = 20;
    private const decimal VipDiscountRate = 0.10m;

    private readonly List<OrderLine> _lines = new();

    public Order(int id, Customer customer, DateOnly date)
    {
        Id = id;
        Customer = customer;
        Date = date;
    }

    public int Id { get; }
    public Customer Customer { get; }
    public DateOnly Date { get; }
    public bool IsPaid { get; private set; }
    public IReadOnlyList<OrderLine> Lines => _lines;

    public decimal Total
    {
        get
        {
            decimal total = _lines.Sum(line => line.LineTotal);

            if (Customer.IsVip)
                total -= total * VipDiscountRate;

            return total;
        }
    }

    public void AddLine(Product product, int quantity)
    {
        if (IsPaid)
            throw new InvalidOperationException("Cannot change a paid order.");

        if (_lines.Count >= MaxLines)
            throw new InvalidOperationException("Order has too many lines.");

        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive.");

        product.ReduceStock(quantity); 
        _lines.Add(new OrderLine(product, quantity));
    }

    public void MarkAsPaid()
    {
        if (_lines.Count == 0)
            throw new InvalidOperationException("Cannot pay an empty order.");

        IsPaid = true;
    }
}
