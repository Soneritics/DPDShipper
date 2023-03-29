namespace DpdShipper.Api.Domain.Shipment.Response;

public class ShipmentResult
{
    public string Consignment { get; set; }

    public IEnumerable<string> LabelNumbers { get; set; }

    public IEnumerable<ShipmentLabelError> Errors { get; set; }
}