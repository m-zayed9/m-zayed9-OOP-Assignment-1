namespace OrderSystem;

public class CustomerList
{
    private readonly List<Customer> _customers = new();

    public IReadOnlyList<Customer> All => _customers;

    public void Add(Customer customer)
    {
        if (FindById(customer.Id) != null)
            throw new InvalidOperationException($"Customer id {customer.Id} already exists.");

        _customers.Add(customer);
    }

    public Customer? FindById(int id) => _customers.FirstOrDefault(c => c.Id == id);
}
