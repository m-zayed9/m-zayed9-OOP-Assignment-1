namespace OrderSystem;

public class Customer
{
    public Customer(int id, string name, string email, string city, bool isVip)
    {
        Id = id;
        Name = name;
        Email = email;
        City = city;
        IsVip = isVip;
    }

    public int Id { get; }
    public string Name { get; }
    public string Email { get; }
    public string City { get; }
    public bool IsVip { get; }

    public override string ToString() =>
        $"#{Id}  {Name}  <{Email}>  {City}  vip={(IsVip ? "yes" : "no")}";
}
