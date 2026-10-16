namespace Part3_BuilderPattern;

public class FlatInvoice
{
    public string InvoiceId { get; internal set; } = "";
    public string CustomerName { get; internal set; } = "";
    public string CustomerEmail { get; internal set; } = "";
    public string CustomerPhone { get; internal set; } = "";

    public string BillingStreet { get; internal set; } = "";
    public string BillingCity { get; internal set; } = "";
    public string BillingState { get; internal set; } = "";
    public string BillingZipCode { get; internal set; } = "";
    public string BillingCountry { get; internal set; } = "";

    public string ShippingStreet { get; internal set; } = "";
    public string ShippingCity { get; internal set; } = "";
    public string ShippingState { get; internal set; } = "";
    public string ShippingZipCode { get; internal set; } = "";
    public string ShippingCountry { get; internal set; } = "";

    public DateTime OrderDate { get; internal set; }
    public string PaymentMethod { get; internal set; } = "";
    public string Currency { get; internal set; } = "";
    public decimal SubTotal { get; internal set; }
    public decimal DiscountAmount { get; internal set; }
    public decimal TaxAmount { get; internal set; }
    public decimal TotalAmount { get; internal set; }
}
