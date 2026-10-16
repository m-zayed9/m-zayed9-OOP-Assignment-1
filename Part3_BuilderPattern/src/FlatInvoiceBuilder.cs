namespace Part3_BuilderPattern;

public class FlatInvoiceBuilder
{
    private string _invoiceId = "";
    private string _customerName = "";
    private string _customerEmail = "";
    private string _customerPhone = "";

    private string _billingStreet = "";
    private string _billingCity = "";
    private string _billingState = "";
    private string _billingZipCode = "";
    private string _billingCountry = "";

    private string _shippingStreet = "";
    private string _shippingCity = "";
    private string _shippingState = "";
    private string _shippingZipCode = "";
    private string _shippingCountry = "";

    private DateTime? _orderDate;
    private string _paymentMethod = "";
    private string _currency = "";
    private decimal? _subTotal;
    private decimal _discountAmount;
    private decimal _taxAmount;

    public FlatInvoiceBuilder WithInvoiceId(string value) { _invoiceId = value; return this; }
    public FlatInvoiceBuilder WithCustomerName(string value) { _customerName = value; return this; }
    public FlatInvoiceBuilder WithCustomerEmail(string value) { _customerEmail = value; return this; }
    public FlatInvoiceBuilder WithCustomerPhone(string value) { _customerPhone = value; return this; }

    public FlatInvoiceBuilder WithBillingStreet(string value) { _billingStreet = value; return this; }
    public FlatInvoiceBuilder WithBillingCity(string value) { _billingCity = value; return this; }
    public FlatInvoiceBuilder WithBillingState(string value) { _billingState = value; return this; }
    public FlatInvoiceBuilder WithBillingZipCode(string value) { _billingZipCode = value; return this; }
    public FlatInvoiceBuilder WithBillingCountry(string value) { _billingCountry = value; return this; }

    public FlatInvoiceBuilder WithShippingStreet(string value) { _shippingStreet = value; return this; }
    public FlatInvoiceBuilder WithShippingCity(string value) { _shippingCity = value; return this; }
    public FlatInvoiceBuilder WithShippingState(string value) { _shippingState = value; return this; }
    public FlatInvoiceBuilder WithShippingZipCode(string value) { _shippingZipCode = value; return this; }
    public FlatInvoiceBuilder WithShippingCountry(string value) { _shippingCountry = value; return this; }

    public FlatInvoiceBuilder WithOrderDate(DateTime value) { _orderDate = value; return this; }
    public FlatInvoiceBuilder WithPaymentMethod(string value) { _paymentMethod = value; return this; }
    public FlatInvoiceBuilder WithCurrency(string value) { _currency = value; return this; }
    public FlatInvoiceBuilder WithSubTotal(decimal value) { _subTotal = value; return this; }
    public FlatInvoiceBuilder WithDiscountAmount(decimal value) { _discountAmount = value; return this; }
    public FlatInvoiceBuilder WithTaxAmount(decimal value) { _taxAmount = value; return this; }

    public FlatInvoice Build()
    {
        // Mandatory: id, name, email, both addresses (except state), date, payment, currency, subtotal.
        // Optional: phone, state, discount, tax.
        List<string> missing = new List<string>();

        if (string.IsNullOrWhiteSpace(_invoiceId)) missing.Add("InvoiceId");
        if (string.IsNullOrWhiteSpace(_customerName)) missing.Add("CustomerName");
        if (string.IsNullOrWhiteSpace(_customerEmail)) missing.Add("CustomerEmail");

        if (string.IsNullOrWhiteSpace(_billingStreet)) missing.Add("BillingStreet");
        if (string.IsNullOrWhiteSpace(_billingCity)) missing.Add("BillingCity");
        if (string.IsNullOrWhiteSpace(_billingZipCode)) missing.Add("BillingZipCode");
        if (string.IsNullOrWhiteSpace(_billingCountry)) missing.Add("BillingCountry");

        if (string.IsNullOrWhiteSpace(_shippingStreet)) missing.Add("ShippingStreet");
        if (string.IsNullOrWhiteSpace(_shippingCity)) missing.Add("ShippingCity");
        if (string.IsNullOrWhiteSpace(_shippingZipCode)) missing.Add("ShippingZipCode");
        if (string.IsNullOrWhiteSpace(_shippingCountry)) missing.Add("ShippingCountry");

        if (_orderDate == null) missing.Add("OrderDate");
        if (string.IsNullOrWhiteSpace(_paymentMethod)) missing.Add("PaymentMethod");
        if (string.IsNullOrWhiteSpace(_currency)) missing.Add("Currency");
        if (_subTotal == null) missing.Add("SubTotal");

        if (missing.Count > 0)
            throw new InvalidOperationException("Cannot build invoice. Missing: " + string.Join(", ", missing));

        if (_subTotal < 0 || _discountAmount < 0 || _taxAmount < 0)
            throw new InvalidOperationException("Amounts cannot be negative.");

        if (_discountAmount > _subTotal)
            throw new InvalidOperationException("Discount cannot be bigger than the subtotal.");

        FlatInvoice invoice = new FlatInvoice();
        invoice.InvoiceId = _invoiceId;
        invoice.CustomerName = _customerName;
        invoice.CustomerEmail = _customerEmail;
        invoice.CustomerPhone = _customerPhone;

        invoice.BillingStreet = _billingStreet;
        invoice.BillingCity = _billingCity;
        invoice.BillingState = _billingState;
        invoice.BillingZipCode = _billingZipCode;
        invoice.BillingCountry = _billingCountry;

        invoice.ShippingStreet = _shippingStreet;
        invoice.ShippingCity = _shippingCity;
        invoice.ShippingState = _shippingState;
        invoice.ShippingZipCode = _shippingZipCode;
        invoice.ShippingCountry = _shippingCountry;

        invoice.OrderDate = _orderDate.Value;
        invoice.PaymentMethod = _paymentMethod;
        invoice.Currency = _currency;
        invoice.SubTotal = _subTotal.Value;
        invoice.DiscountAmount = _discountAmount;
        invoice.TaxAmount = _taxAmount;
        invoice.TotalAmount = _subTotal.Value - _discountAmount + _taxAmount;

        return invoice;
    }
}
