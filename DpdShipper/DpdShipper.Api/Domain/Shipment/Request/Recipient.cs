namespace DpdShipper.Api.Domain.Shipment.Request;

public class Recipient
{
    public string Name { get; set; }
    public string? Company { get; set; }
    public string Street { get; set; }
    public string HouseNumber { get; set; }
    public string? Street2 { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
    public string CountryCode { get; set; }
    public string? Email { get; set; }
    public RecipientTypes RecipientType { get; set; }
}