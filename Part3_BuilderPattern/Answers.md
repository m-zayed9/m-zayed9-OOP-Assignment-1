# Part 3 - Answers

## Task 3.1

### Q1. Why is a single 20-parameter constructor a problem?

At the call site it looks like this: `new Invoice("INV-1", "Mona", "m@x.com", "010...", "12 Nile", "Cairo", "Cairo", "11511", "Egypt", ...)`.
I can't tell what the 6th or 7th value is without opening the class. Because many parameters have the same type
(lots of `string`, and `SubTotal`, `DiscountAmount`, `TaxAmount` are all `decimal`) I can swap two of them and the
code still compiles: for example putting the city in the state position, the billing street in the shipping street
position, or the tax in the discount position. The mistake only appears later as a wrong invoice.

Optional values are also a problem, because the caller still has to pass something (`""` or `null`) for every
one of them. And when someone adds one more property, the constructor signature changes, so every place that
creates an invoice stops compiling (or, if a default value is added, quietly passes the wrong thing).

### Q2. Is it only a long-constructor problem?

No. The long constructor is a symptom. The class is doing too much: it contains the customer, the billing address,
the shipping address and the payment details all together. The two addresses are the same idea copied twice
(`BillingStreet` / `ShippingStreet`, ...), so the same rules and the same fields are repeated. The properties are
only loosely related, which means the class has many reasons to change. The better design is to group related
values into their own small classes (`Address`, `OrderInfo`) and let the invoice contain them.

## Task 3.2 - notes

In `FlatInvoiceBuilder` the mandatory values are: invoice id, customer name, customer email, the address fields
(street, city, zip code, country) for billing and shipping, order date, payment method, currency and subtotal.
The optional values are: phone, state, discount and tax (they default to empty / 0). `Build()` collects every
missing mandatory value and throws one exception that lists all of them, so the caller gets a clear error
immediately. `TotalAmount` is calculated by the builder, so it can never be inconsistent with the other amounts.

## Task 3.3 - Why is the composed version better?

**Single responsibility.** `AddressBuilder` only knows about addresses, `OrderBuilder` only knows about order and
payment values, and `InvoiceBuilder` only joins the pieces together and checks the customer data. When a rule
about addresses changes, I only change `AddressBuilder`.

**Independent validation.** `AddressBuilder.Build()` refuses to create an address without a street, city, zip code
or country. The invoice does not need to know any of those rules, and I can create and test an address alone.
Any `Address` object that exists is already complete. The same applies to `OrderBuilder` (no negative amounts,
discount not bigger than the subtotal).

**Reuse.** The same `AddressBuilder` creates both the billing and the shipping address. Without it I would have
to write the fields, setters and validation twice (once with `Billing...` names and once with `Shipping...`
names) and keep both copies in sync. Also the `Address` class could be reused for a `Customer` or a `Supplier` later.

**Readability at the call site.** In Task 3.2 the call is one long chain of about 17 `With...` calls, where the
billing and shipping lines are mixed in a long list. In the composed version I first build `billing`, `shipping`
and `order` as separate named variables and the final call is only 6 lines:
`new InvoiceBuilder().WithInvoiceId(...).WithCustomerName(...).WithBillingAddress(billing).WithShippingAddress(shipping).WithOrder(order).Build()`.
It is easy to see which part is which, and it is not possible to mix up a billing value with a shipping value.
