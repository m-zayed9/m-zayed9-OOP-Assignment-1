namespace OrderSystem;

public class Product
{
    public Product(int id, string name, decimal price, int stock)
    {
        Id = id;
        Name = name;
        Price = price;
        Stock = stock;
    }

    public int Id { get; }
    public string Name { get; }
    public decimal Price { get; }
    public int Stock { get; private set; }

    public void ReduceStock(int quantity)
    {
        if (quantity > Stock)
            throw new InvalidOperationException($"Not enough stock for product #{Id}.");

        Stock -= quantity;
    }

    public override string ToString() =>
        $"#{Id}  {Name}  price={Price:F2}  stock={Stock}";
}
