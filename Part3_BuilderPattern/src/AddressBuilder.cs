namespace Part3_BuilderPattern;

public class AddressBuilder
{
    private string _street = "";
    private string _city = "";
    private string _state = "";
    private string _zipCode = "";
    private string _country = "";

    public AddressBuilder WithStreet(string value) { _street = value; return this; }
    public AddressBuilder WithCity(string value) { _city = value; return this; }
    public AddressBuilder WithState(string value) { _state = value; return this; }
    public AddressBuilder WithZipCode(string value) { _zipCode = value; return this; }
    public AddressBuilder WithCountry(string value) { _country = value; return this; }

    public Address Build()
    {
        List<string> missing = new List<string>();

        if (string.IsNullOrWhiteSpace(_street)) missing.Add("Street");
        if (string.IsNullOrWhiteSpace(_city)) missing.Add("City");
        if (string.IsNullOrWhiteSpace(_zipCode)) missing.Add("ZipCode");
        if (string.IsNullOrWhiteSpace(_country)) missing.Add("Country");

        if (missing.Count > 0)
            throw new InvalidOperationException("Incomplete address. Missing: " + string.Join(", ", missing));

        return new Address(_street, _city, _state, _zipCode, _country);
    }
}
