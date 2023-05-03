namespace DpdShipper.Api.Domain.Shipment.Request;

public class Label
{
    public string? ShipmentReferenceNumber { get; set; }
    public Products Product { get; set; } = Products.CL;
    public bool SaturdayDelivery { get; set; } = false;
    public Sender Sender { get; set; }
    public Recipient Recipient { get; set; }
    public IEnumerable<Parcel> Parcels { get; set; }
}