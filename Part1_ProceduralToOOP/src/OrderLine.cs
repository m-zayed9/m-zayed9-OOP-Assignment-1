namespace OrderSystem;

public class OrderLine
{
    public OrderLine(Product product, int quantity)
    {
        Product = product;
        Quantity = quantity;
        UnitPrice = product.Price;
    }

    public Product Product { get; }
    public int Quantity { get; }
    public decimal UnitPrice { get; }
    public decimal LineTotal => UnitPrice * Quantity;
}
