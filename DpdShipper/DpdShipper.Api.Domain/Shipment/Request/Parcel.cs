namespace DpdShipper.Api.Domain.Shipment.Request;

public class Parcel
{
    public string CustomerReferenceNumber { get; set; }
    public int Weight { get; set; } = 0;
}