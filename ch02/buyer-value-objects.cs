// Entity
// modeled with value objects
class Buyer
{
    // Entity identifier
    public int Id { get; set; }
    // Buyer name
    public string Name { get; set; }
    // Billing address
    public List<Address> Addresses { get; set; }
}

struct Address
{
    public AddressType AddressType { get; set; }
    public string Line1 { get; set; }
    public string Line2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
}

enum AddressType
{
    Billing,
    Shipping
}