namespace DpdShipper.Api.Domain.Shipment.Response;

public class ShipmentResults
{
    public byte[]? ResultFile { get; set; }

    public List<ShipmentResult> ShipmentResultList { get; set; }
}