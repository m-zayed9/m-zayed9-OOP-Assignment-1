namespace Part3_BuilderPattern;

public class Program
{
    public static void Main()
    {
        FlatInvoice flat = new FlatInvoiceBuilder()
            .WithInvoiceId("INV-001")
            .WithCustomerName("Mona Ali")
            .WithCustomerEmail("mona@example.com")
            .WithBillingStreet("12 Nile St")
            .WithBillingCity("Cairo")
            .WithBillingZipCode("11511")
            .WithBillingCountry("Egypt")
            .WithShippingStreet("5 Garden St")
            .WithShippingCity("Giza")
            .WithShippingZipCode("12511")
            .WithShippingCountry("Egypt")
            .WithOrderDate(new DateTime(2026, 10, 16))
            .WithPaymentMethod("Card")
            .WithCurrency("EGP")
            .WithSubTotal(1000m)
            .WithDiscountAmount(100m)
            .WithTaxAmount(126m)
            .Build();

        Console.WriteLine("Flat invoice " + flat.InvoiceId + " total: " + flat.TotalAmount + " " + flat.Currency);

        try
        {
            new FlatInvoiceBuilder().WithInvoiceId("INV-002").WithCustomerName("Omar").Build();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("Rejected: " + ex.Message);
        }

        // ---- Task 3.3: composed builders ----
        Address billing = new AddressBuilder()
            .WithStreet("12 Nile St")
            .WithCity("Cairo")
            .WithZipCode("11511")
            .WithCountry("Egypt")
            .Build();

        Address shipping = new AddressBuilder()
            .WithStreet("5 Garden St")
            .WithCity("Giza")
            .WithState("Giza")
            .WithZipCode("12511")
            .WithCountry("Egypt")
            .Build();

        OrderInfo order = new OrderBuilder()
            .WithOrderDate(new DateTime(2026, 10, 16))
            .WithPaymentMethod("Card")
            .WithCurrency("EGP")
            .WithSubTotal(1000m)
            .WithDiscount(100m)
            .WithTax(126m)
            .Build();

        Invoice invoice = new InvoiceBuilder()
            .WithInvoiceId("INV-003")
            .WithCustomerName("Mona Ali")
            .WithCustomerEmail("mona@example.com")
            .WithBillingAddress(billing)
            .WithShippingAddress(shipping)
            .WithOrder(order)
            .Build();

        Console.WriteLine("Invoice " + invoice.InvoiceId + " total: " + invoice.Order.TotalAmount + " " + invoice.Order.Currency);
        Console.WriteLine("Billing:  " + invoice.BillingAddress);
        Console.WriteLine("Shipping: " + invoice.ShippingAddress);

        try
        {
            new AddressBuilder().WithStreet("1 Some St").Build();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("Rejected: " + ex.Message);
        }
    }
}
