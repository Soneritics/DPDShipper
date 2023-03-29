namespace DpdShipper.Api.Domain.Shipment.Request;

public class Parcel
{
    public string ParcelSpecificReferenceNumber { get; set; }
    public int Weight { get; set; } = 0;
}