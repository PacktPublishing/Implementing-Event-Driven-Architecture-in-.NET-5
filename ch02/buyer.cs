// Entity
// modeled without value objects
class Buyer
{
    // Entity identifier
    public int Id { get; set; }
    // Buyer name
    public string Name { get; set; }
    // Billing address
    public string BillingAddressLine1 { get; set; }
    public string BillingAddressLine2 { get; set; }
    public string BillingAddressCity { get; set; }
    public string BillingAddressState { get; set; }
    public string BillingAddressZip { get; set; }
    // Shipping address
    public string ShippingAddressLine1 { get; set; }
    public string ShippingAddressLine2 { get; set; }
    public string ShippingAddressCity { get; set; }
    public string ShippingAddressState { get; set; }
    public string ShippingAddressZip { get; set; }
}
