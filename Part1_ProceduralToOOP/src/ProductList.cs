namespace OrderSystem;

public class ProductList
{
    private readonly List<Product> _products = new();

    public IReadOnlyList<Product> All => _products;

    public void Add(Product product)
    {
        if (FindById(product.Id) != null)
            throw new InvalidOperationException($"Product id {product.Id} already exists.");

        _products.Add(product);
    }

    public Product? FindById(int id) => _products.FirstOrDefault(p => p.Id == id);
}
