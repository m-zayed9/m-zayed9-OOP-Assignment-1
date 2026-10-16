namespace Part3_BuilderPattern;

public class InvoiceBuilder
{
    private string _invoiceId = "";
    private string _customerName = "";
    private string _customerEmail = "";
    private string _customerPhone = "";
    private Address? _billingAddress;
    private Address? _shippingAddress;
    private OrderInfo? _order;

    public InvoiceBuilder WithInvoiceId(string value) { _invoiceId = value; return this; }
    public InvoiceBuilder WithCustomerName(string value) { _customerName = value; return this; }
    public InvoiceBuilder WithCustomerEmail(string value) { _customerEmail = value; return this; }
    public InvoiceBuilder WithCustomerPhone(string value) { _customerPhone = value; return this; }   // optional
    public InvoiceBuilder WithBillingAddress(Address value) { _billingAddress = value; return this; }
    public InvoiceBuilder WithShippingAddress(Address value) { _shippingAddress = value; return this; }
    public InvoiceBuilder WithOrder(OrderInfo value) { _order = value; return this; }

    public Invoice Build()
    {
        List<string> missing = new List<string>();

        if (string.IsNullOrWhiteSpace(_invoiceId)) missing.Add("InvoiceId");
        if (string.IsNullOrWhiteSpace(_customerName)) missing.Add("CustomerName");
        if (string.IsNullOrWhiteSpace(_customerEmail)) missing.Add("CustomerEmail");
        if (_billingAddress == null) missing.Add("BillingAddress");
        if (_shippingAddress == null) missing.Add("ShippingAddress");
        if (_order == null) missing.Add("Order");

        if (missing.Count > 0)
            throw new InvalidOperationException("Cannot build invoice. Missing: " + string.Join(", ", missing));

        return new Invoice(_invoiceId, _customerName, _customerEmail, _customerPhone,
                           _billingAddress!, _shippingAddress!, _order!);
    }
}
