namespace DpdShipper.Api.Domain.Shipment.Request;

public class Sender
{
    public string Name { get; set; }
    public string Street { get; set; }
    public string HouseNumber { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
    public string CountryCode { get; set; }
}