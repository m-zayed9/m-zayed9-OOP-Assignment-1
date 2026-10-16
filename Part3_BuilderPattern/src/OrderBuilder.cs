namespace Part3_BuilderPattern;

public class OrderBuilder
{
    private DateTime? _orderDate;
    private string _paymentMethod = "";
    private string _currency = "";
    private decimal? _subTotal;
    private decimal _discountAmount;
    private decimal _taxAmount;

    public OrderBuilder WithOrderDate(DateTime value) { _orderDate = value; return this; }
    public OrderBuilder WithPaymentMethod(string value) { _paymentMethod = value; return this; }
    public OrderBuilder WithCurrency(string value) { _currency = value; return this; }
    public OrderBuilder WithSubTotal(decimal value) { _subTotal = value; return this; }
    public OrderBuilder WithDiscount(decimal value) { _discountAmount = value; return this; }   // optional
    public OrderBuilder WithTax(decimal value) { _taxAmount = value; return this; }             // optional

    public OrderInfo Build()
    {
        List<string> missing = new List<string>();

        if (_orderDate == null) missing.Add("OrderDate");
        if (string.IsNullOrWhiteSpace(_paymentMethod)) missing.Add("PaymentMethod");
        if (string.IsNullOrWhiteSpace(_currency)) missing.Add("Currency");
        if (_subTotal == null) missing.Add("SubTotal");

        if (missing.Count > 0)
            throw new InvalidOperationException("Incomplete order info. Missing: " + string.Join(", ", missing));

        if (_subTotal < 0 || _discountAmount < 0 || _taxAmount < 0)
            throw new InvalidOperationException("Amounts cannot be negative.");

        if (_discountAmount > _subTotal)
            throw new InvalidOperationException("Discount cannot be bigger than the subtotal.");

        return new OrderInfo(_orderDate!.Value, _paymentMethod, _currency,
                             _subTotal!.Value, _discountAmount, _taxAmount);
    }
}
